using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

namespace Game.Level.Editor
{
    [MovedFrom("")]
    public static class RoomTilemapConsolidator
    {
        private const string MenuBase = "Tools/Level/Consolidate Room Tilemaps/";
        private const string WorldRootName = "WorldTilemaps";

        private static readonly Dictionary<string, int> LayerOrderByName = new Dictionary<string, int>
        {
            { "Background", -100 },
            { "Ground", -20 },
            { "Platforms", -10 },
            { "Walls", 0 },
            { "Bounds", 0 },
            { "Obstacles", 5 },
            { "Ceiling", 10 }
        };

        [MenuItem(MenuBase + "Keep Sources")]
        private static void ConsolidateKeepSources() => Consolidate(disableSources: false);

        [MenuItem(MenuBase + "Disable Sources")]
        private static void ConsolidateDisableSources() => Consolidate(disableSources: true);

        private static void Consolidate(bool disableSources)
        {
            var roomsRoot = ResolveRoomsRoot();
            if (roomsRoot == null)
            {
                EditorUtility.DisplayDialog(
                    "Consolidation Failed",
                    "Select the Rooms object (or create object named 'Rooms') and retry.",
                    "OK");
                return;
            }

            var sources = roomsRoot.GetComponentsInChildren<Tilemap>(true);
            if (sources == null || sources.Length == 0)
            {
                EditorUtility.DisplayDialog(
                    "Consolidation Failed",
                    "No Tilemap components found under selected Rooms root.",
                    "OK");
                return;
            }

            var worldRoot = GetOrCreateWorldRoot(roomsRoot);
            var worldGrid = EnsureWorldGrid(worldRoot, sources);

            var groupedSources = GroupSourcesByLayer(sources, worldRoot, roomsRoot);
            if (groupedSources.Count == 0)
            {
                EditorUtility.DisplayDialog(
                    "Consolidation Failed",
                    "No source tilemaps left to consolidate (possible cause: only WorldTilemaps selected).",
                    "OK");
                return;
            }

            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName("Consolidate Room Tilemaps");
            var undoGroup = Undo.GetCurrentGroup();

            var copiedTotal = 0;
            foreach (var kv in groupedSources)
            {
                var layerName = kv.Key;
                var layerSources = kv.Value;
                var exemplar = layerSources[0];

                var target = GetOrCreateLayerTilemap(worldRoot, layerName, exemplar);
                Undo.RecordObject(target, "Clear target tilemap");
                target.ClearAllTiles();

                var copiedInLayer = 0;
                for (var i = 0; i < layerSources.Count; i++)
                {
                    copiedInLayer += CopyTiles(layerSources[i], target);
                    if (disableSources)
                    {
                        DisableSourceTilemap(layerSources[i]);
                    }
                }

                copiedTotal += copiedInLayer;
                target.CompressBounds();
            }

            Undo.RecordObject(worldGrid, "Touch world grid");

            var scene = worldRoot.gameObject.scene;
            if (scene.IsValid())
            {
                EditorSceneManager.MarkSceneDirty(scene);
            }

            Undo.CollapseUndoOperations(undoGroup);
            Selection.activeGameObject = worldRoot.gameObject;

            Debug.Log(
                $"[RoomTilemapConsolidator] Done. Layers: {groupedSources.Count}, copied tiles: {copiedTotal}, disableSources: {disableSources}.",
                worldRoot);
        }

        private static Transform ResolveRoomsRoot()
        {
            if (Selection.activeTransform != null)
            {
                return Selection.activeTransform;
            }

            var byName = GameObject.Find("Rooms");
            return byName != null ? byName.transform : null;
        }

        private static Transform GetOrCreateWorldRoot(Transform roomsRoot)
        {
            var parent = roomsRoot.parent != null ? roomsRoot.parent : roomsRoot;
            var existing = parent.Find(WorldRootName);
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject(WorldRootName).transform;
            Undo.RegisterCreatedObjectUndo(root.gameObject, "Create world tilemaps root");
            root.SetParent(parent, false);
            root.localPosition = Vector3.zero;
            root.localRotation = Quaternion.identity;
            root.localScale = Vector3.one;
            return root;
        }

        private static Grid EnsureWorldGrid(Transform worldRoot, Tilemap[] sources)
        {
            var grid = worldRoot.GetComponent<Grid>();
            if (grid == null)
            {
                grid = Undo.AddComponent<Grid>(worldRoot.gameObject);
            }

            var sourceGrid = FindAnySourceGrid(sources);
            if (sourceGrid != null)
            {
                Undo.RecordObject(grid, "Copy grid settings");
                grid.cellSize = sourceGrid.cellSize;
                grid.cellGap = sourceGrid.cellGap;
                grid.cellLayout = sourceGrid.cellLayout;
                grid.cellSwizzle = sourceGrid.cellSwizzle;
            }

            return grid;
        }

        private static Grid FindAnySourceGrid(Tilemap[] sources)
        {
            for (var i = 0; i < sources.Length; i++)
            {
                var grid = sources[i].GetComponentInParent<Grid>();
                if (grid != null)
                {
                    return grid;
                }
            }

            return null;
        }

        private static Dictionary<string, List<Tilemap>> GroupSourcesByLayer(Tilemap[] sources, Transform worldRoot, Transform roomsRoot)
        {
            var grouped = new Dictionary<string, List<Tilemap>>();
            for (var i = 0; i < sources.Length; i++)
            {
                var source = sources[i];
                if (source == null)
                {
                    continue;
                }

                if (source.transform.IsChildOf(worldRoot))
                {
                    continue;
                }

                var key = ResolveLayerKey(source, roomsRoot);

                if (!grouped.TryGetValue(key, out var list))
                {
                    list = new List<Tilemap>();
                    grouped.Add(key, list);
                }

                list.Add(source);
            }

            return grouped;
        }

        private static string ResolveLayerKey(Tilemap source, Transform roomsRoot)
        {
            if (IsBoundsTilemap(source, roomsRoot))
            {
                return "Bounds";
            }

            var key = source.gameObject.name.Trim();
            if (string.IsNullOrEmpty(key))
            {
                key = "Layer";
            }

            return key;
        }

        private static bool IsBoundsTilemap(Tilemap source, Transform roomsRoot)
        {
            var cursor = source.transform.parent;
            while (cursor != null)
            {
                var name = cursor.name.ToLowerInvariant();
                if (name == "bounds" || name == "level bounds" || name.Contains("bound"))
                {
                    return true;
                }

                if (roomsRoot != null && cursor == roomsRoot)
                {
                    break;
                }

                cursor = cursor.parent;
            }

            return false;
        }

        private static Tilemap GetOrCreateLayerTilemap(Transform worldRoot, string layerName, Tilemap exemplar)
        {
            var existing = worldRoot.Find(layerName);
            GameObject layerObject;
            if (existing == null)
            {
                layerObject = new GameObject(layerName);
                Undo.RegisterCreatedObjectUndo(layerObject, $"Create {layerName} tilemap");
                layerObject.transform.SetParent(worldRoot, false);
            }
            else
            {
                layerObject = existing.gameObject;
            }

            layerObject.transform.localPosition = Vector3.zero;
            layerObject.transform.localRotation = Quaternion.identity;
            layerObject.transform.localScale = Vector3.one;

            var targetTilemap = layerObject.GetComponent<Tilemap>();
            if (targetTilemap == null)
            {
                targetTilemap = Undo.AddComponent<Tilemap>(layerObject);
            }

            var targetRenderer = layerObject.GetComponent<TilemapRenderer>();
            if (targetRenderer == null)
            {
                targetRenderer = Undo.AddComponent<TilemapRenderer>(layerObject);
            }

            CopyTilemapSettings(exemplar, targetTilemap);
            CopyRendererSettings(exemplar, targetRenderer, layerName);
            CopyColliderStack(exemplar.gameObject, layerObject);

            return targetTilemap;
        }

        private static void CopyTilemapSettings(Tilemap source, Tilemap target)
        {
            Undo.RecordObject(target, "Copy tilemap settings");
            target.color = source.color;
            target.animationFrameRate = source.animationFrameRate;
            target.tileAnchor = source.tileAnchor;
            target.orientation = source.orientation;
            target.orientationMatrix = source.orientationMatrix;
        }

        private static void CopyRendererSettings(Tilemap source, TilemapRenderer targetRenderer, string layerName)
        {
            var sourceRenderer = source.GetComponent<TilemapRenderer>();
            if (sourceRenderer == null)
            {
                return;
            }

            Undo.RecordObject(targetRenderer, "Copy renderer settings");
            targetRenderer.sortOrder = sourceRenderer.sortOrder;
            targetRenderer.mode = sourceRenderer.mode;
            targetRenderer.maskInteraction = sourceRenderer.maskInteraction;
            targetRenderer.sharedMaterial = sourceRenderer.sharedMaterial;

            targetRenderer.sortingLayerID = sourceRenderer.sortingLayerID;
            targetRenderer.sortingOrder = ResolveSortingOrder(layerName, sourceRenderer.sortingOrder);
        }

        private static int ResolveSortingOrder(string layerName, int fallback)
        {
            if (LayerOrderByName.TryGetValue(layerName, out var order))
            {
                return order;
            }

            return fallback;
        }

        private static void CopyColliderStack(GameObject sourceObject, GameObject targetObject)
        {
            var sourceTilemapCollider = sourceObject.GetComponent<TilemapCollider2D>();
            var sourceComposite = sourceObject.GetComponent<CompositeCollider2D>();

            var targetTilemapCollider = targetObject.GetComponent<TilemapCollider2D>();
            if (sourceTilemapCollider != null)
            {
                if (targetTilemapCollider == null)
                {
                    targetTilemapCollider = Undo.AddComponent<TilemapCollider2D>(targetObject);
                }

                Undo.RecordObject(targetTilemapCollider, "Copy tilemap collider settings");
                targetTilemapCollider.isTrigger = sourceTilemapCollider.isTrigger;
                targetTilemapCollider.usedByComposite = sourceTilemapCollider.usedByComposite;
                targetTilemapCollider.offset = sourceTilemapCollider.offset;
            }
            else if (targetTilemapCollider != null)
            {
                Undo.DestroyObjectImmediate(targetTilemapCollider);
            }

            var targetComposite = targetObject.GetComponent<CompositeCollider2D>();
            var targetRigidbody = targetObject.GetComponent<Rigidbody2D>();

            if (sourceComposite != null)
            {
                if (targetRigidbody == null)
                {
                    targetRigidbody = Undo.AddComponent<Rigidbody2D>(targetObject);
                }

                Undo.RecordObject(targetRigidbody, "Copy rigidbody settings");
                targetRigidbody.bodyType = RigidbodyType2D.Static;
                targetRigidbody.simulated = true;

                if (targetComposite == null)
                {
                    targetComposite = Undo.AddComponent<CompositeCollider2D>(targetObject);
                }

                Undo.RecordObject(targetComposite, "Copy composite settings");
                targetComposite.geometryType = sourceComposite.geometryType;
                targetComposite.vertexDistance = sourceComposite.vertexDistance;
                targetComposite.edgeRadius = sourceComposite.edgeRadius;
                targetComposite.offset = sourceComposite.offset;
                targetComposite.isTrigger = sourceComposite.isTrigger;
            }
            else
            {
                if (targetComposite != null)
                {
                    Undo.DestroyObjectImmediate(targetComposite);
                }

                if (targetRigidbody != null)
                {
                    Undo.DestroyObjectImmediate(targetRigidbody);
                }
            }
        }

        private static int CopyTiles(Tilemap source, Tilemap target)
        {
            var copied = 0;
            var bounds = source.cellBounds;
            foreach (var cell in bounds.allPositionsWithin)
            {
                if (!source.HasTile(cell))
                {
                    continue;
                }

                var tile = source.GetTile(cell);
                if (tile == null)
                {
                    continue;
                }

                var world = source.GetCellCenterWorld(cell);
                var targetCell = target.WorldToCell(world);

                target.SetTile(targetCell, tile);

                var sourceFlags = source.GetTileFlags(cell);
                target.SetTileFlags(targetCell, TileFlags.None);
                target.SetColor(targetCell, source.GetColor(cell));
                target.SetTransformMatrix(targetCell, source.GetTransformMatrix(cell));
                target.SetTileFlags(targetCell, sourceFlags);

                copied++;
            }

            return copied;
        }

        private static void DisableSourceTilemap(Tilemap source)
        {
            var renderer = source.GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                Undo.RecordObject(renderer, "Disable source renderer");
                renderer.enabled = false;
            }

            var tilemapCollider = source.GetComponent<TilemapCollider2D>();
            if (tilemapCollider != null)
            {
                Undo.RecordObject(tilemapCollider, "Disable source tilemap collider");
                tilemapCollider.enabled = false;
            }

            var composite = source.GetComponent<CompositeCollider2D>();
            if (composite != null)
            {
                Undo.RecordObject(composite, "Disable source composite collider");
                composite.enabled = false;
            }
        }
    }
}

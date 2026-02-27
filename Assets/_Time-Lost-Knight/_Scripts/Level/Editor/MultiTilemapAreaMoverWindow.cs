using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public sealed class MultiTilemapAreaMoverWindow : EditorWindow
{
    private const string MenuPath = "Tools/Level/Multi Tilemap Area Mover";

    [Serializable]
    private sealed class TilemapEntry
    {
        public Tilemap tilemap;
        public bool enabled = true;
    }

    private struct TileSnapshot
    {
        public Vector3Int SourceCell;
        public Vector3Int DestinationCell;
        public TileBase Tile;
        public TileFlags Flags;
        public Color Color;
        public Matrix4x4 Transform;
    }

    [SerializeField] private Transform _tilemapsRoot;
    [SerializeField] private Transform _objectsRoot;
    [SerializeField] private List<TilemapEntry> _tilemaps = new List<TilemapEntry>();
    [SerializeField] private BoundsInt _selection = new BoundsInt(Vector3Int.zero, new Vector3Int(1, 1, 1));
    [SerializeField] private Vector3Int _moveOffset = Vector3Int.right;
    [SerializeField] private bool _moveObjects = true;
    [SerializeField] private bool _includeInactiveObjects = true;
    [SerializeField] private bool _useBoundsForObjects = true;
    [SerializeField] private bool _cursorMoveMode;
    [SerializeField] private Vector2 _scroll;

    private bool _isSceneDragging;
    private Vector3Int _dragStartCell;
    private Vector3Int _previewOffset;

    [MenuItem(MenuPath)]
    private static void Open()
    {
        GetWindow<MultiTilemapAreaMoverWindow>("Area Mover");
    }

    private void OnEnable()
    {
        AutoResolveRoots();
        RefreshTilemaps();
        SceneView.duringSceneGui += OnSceneGUI;

        if (GridSelection.active)
        {
            CaptureSelectionFromGridSelection(showErrors: false);
        }
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        _isSceneDragging = false;
        _previewOffset = Vector3Int.zero;
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox(
            "Select an area with the Tilemap Select Tool (or enter Bounds manually), choose layers and offset, then click Move Selection.",
            MessageType.Info);

        DrawRootsPanel();
        EditorGUILayout.Space(6f);

        DrawSelectionPanel();
        EditorGUILayout.Space(6f);

        DrawTilemapsPanel();
        EditorGUILayout.Space(6f);

        DrawObjectsPanel();
        EditorGUILayout.Space(8f);

        DrawCursorPanel();
        EditorGUILayout.Space(8f);

        using (new EditorGUI.DisabledScope(!CanExecuteMove()))
        {
            if (GUILayout.Button("Move Selection", GUILayout.Height(30f)))
            {
                ExecuteMove();
            }
        }
    }

    private void DrawRootsPanel()
    {
        EditorGUILayout.LabelField("Roots", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        _tilemapsRoot = (Transform)EditorGUILayout.ObjectField("Tilemaps Root", _tilemapsRoot, typeof(Transform), true);
        if (EditorGUI.EndChangeCheck())
        {
            RefreshTilemaps();
            if (_objectsRoot == null && _tilemapsRoot != null)
            {
                _objectsRoot = _tilemapsRoot.parent != null ? _tilemapsRoot.parent : _tilemapsRoot;
            }
        }

        _objectsRoot = (Transform)EditorGUILayout.ObjectField("Objects Root", _objectsRoot, typeof(Transform), true);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Auto Detect"))
            {
                AutoResolveRoots();
                RefreshTilemaps();
            }

            if (GUILayout.Button("Refresh Tilemaps"))
            {
                RefreshTilemaps();
            }
        }
    }

    private void DrawSelectionPanel()
    {
        EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);

        if (GridSelection.active)
        {
            var targetName = GridSelection.target != null ? GridSelection.target.name : "(null)";
            EditorGUILayout.HelpBox($"Grid Selection is active. Target: {targetName}", MessageType.None);
        }

        if (GUILayout.Button("Use Active Grid Selection"))
        {
            CaptureSelectionFromGridSelection(showErrors: true);
        }

        _selection = EditorGUILayout.BoundsIntField("Selected Bounds", _selection);
        _moveOffset = EditorGUILayout.Vector3IntField("Move Offset (Cells)", _moveOffset);
    }

    private void DrawTilemapsPanel()
    {
        EditorGUILayout.LabelField("Tilemaps", EditorStyles.boldLabel);

        if (_tilemaps.Count == 0)
        {
            EditorGUILayout.HelpBox("No Tilemaps found under Tilemaps Root.", MessageType.Warning);
            return;
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Enable All"))
            {
                SetAllTilemapsEnabled(true);
            }

            if (GUILayout.Button("Disable All"))
            {
                SetAllTilemapsEnabled(false);
            }
        }

        var maxHeight = Mathf.Min(220f, 28f + (_tilemaps.Count * 21f));
        _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(maxHeight));
        for (var i = 0; i < _tilemaps.Count; i++)
        {
            var entry = _tilemaps[i];
            if (entry == null || entry.tilemap == null)
            {
                continue;
            }

            var label = BuildTilemapLabel(entry.tilemap);
            entry.enabled = EditorGUILayout.ToggleLeft(label, entry.enabled);
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawObjectsPanel()
    {
        EditorGUILayout.LabelField("Objects", EditorStyles.boldLabel);

        _moveObjects = EditorGUILayout.Toggle("Move Scene Objects", _moveObjects);

        using (new EditorGUI.DisabledScope(!_moveObjects))
        {
            _includeInactiveObjects = EditorGUILayout.Toggle("Include Inactive", _includeInactiveObjects);
            _useBoundsForObjects = EditorGUILayout.Toggle("Use Bounds Overlap", _useBoundsForObjects);
        }
    }

    private void DrawCursorPanel()
    {
        EditorGUILayout.LabelField("Cursor Move", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        _cursorMoveMode = EditorGUILayout.Toggle("Enable Scene Drag", _cursorMoveMode);
        if (EditorGUI.EndChangeCheck() && !_cursorMoveMode)
        {
            _isSceneDragging = false;
            _previewOffset = Vector3Int.zero;
        }

        EditorGUILayout.HelpBox(
            "When enabled: drag with left mouse inside selected area in Scene View. Release to apply move. Esc cancels current drag.",
            MessageType.None);

        if (_cursorMoveMode)
        {
            EditorGUILayout.LabelField("Live Offset", _previewOffset.ToString());
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!_cursorMoveMode)
        {
            return;
        }

        if (_selection.size.x <= 0 || _selection.size.y <= 0 || _selection.size.z <= 0)
        {
            return;
        }

        var enabledTilemaps = GetEnabledTilemaps();
        var grid = ResolveReferenceGrid(enabledTilemaps);
        if (grid == null)
        {
            return;
        }

        DrawBoundsOutline(grid, _selection, new Color(0.05f, 0.8f, 1f, 1f), 2f);
        if (_isSceneDragging && _previewOffset != Vector3Int.zero)
        {
            var previewBounds = OffsetBounds(_selection, _previewOffset);
            DrawBoundsOutline(grid, previewBounds, new Color(1f, 0.7f, 0.15f, 1f), 3f);
        }

        var evt = Event.current;
        if (evt == null)
        {
            return;
        }

        if (evt.type == EventType.KeyDown && evt.keyCode == KeyCode.Escape)
        {
            _isSceneDragging = false;
            _previewOffset = Vector3Int.zero;
            _moveOffset = Vector3Int.zero;
            evt.Use();
            sceneView.Repaint();
            Repaint();
            return;
        }

        if (_isSceneDragging)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }

        if (evt.alt || evt.button != 0)
        {
            return;
        }

        switch (evt.type)
        {
            case EventType.MouseDown:
                if (TryGetMouseCell(grid, evt.mousePosition, out var startCell) && _selection.Contains(startCell))
                {
                    _isSceneDragging = true;
                    _dragStartCell = startCell;
                    _previewOffset = Vector3Int.zero;
                    _moveOffset = Vector3Int.zero;
                    evt.Use();
                    sceneView.Repaint();
                    Repaint();
                }
                break;

            case EventType.MouseDrag:
            case EventType.MouseMove:
                if (!_isSceneDragging)
                {
                    break;
                }

                if (TryGetMouseCell(grid, evt.mousePosition, out var currentCell))
                {
                    _previewOffset = currentCell - _dragStartCell;
                    _moveOffset = _previewOffset;
                    evt.Use();
                    sceneView.Repaint();
                    Repaint();
                }
                break;

            case EventType.MouseUp:
                if (!_isSceneDragging)
                {
                    break;
                }

                var finalOffset = _previewOffset;
                _isSceneDragging = false;
                _previewOffset = Vector3Int.zero;
                _moveOffset = finalOffset;

                if (finalOffset != Vector3Int.zero)
                {
                    ExecuteMove();
                }

                evt.Use();
                sceneView.Repaint();
                Repaint();
                break;
        }
    }

    private static BoundsInt OffsetBounds(BoundsInt source, Vector3Int offset)
    {
        var result = source;
        result.position += offset;
        return result;
    }

    private static void DrawBoundsOutline(Grid grid, BoundsInt bounds, Color color, float thickness)
    {
        var z = bounds.zMin;
        var p00 = grid.CellToWorld(new Vector3Int(bounds.xMin, bounds.yMin, z));
        var p10 = grid.CellToWorld(new Vector3Int(bounds.xMax, bounds.yMin, z));
        var p11 = grid.CellToWorld(new Vector3Int(bounds.xMax, bounds.yMax, z));
        var p01 = grid.CellToWorld(new Vector3Int(bounds.xMin, bounds.yMax, z));

        var depthOffset = grid.transform.forward * -0.01f;
        p00 += depthOffset;
        p10 += depthOffset;
        p11 += depthOffset;
        p01 += depthOffset;

        Handles.color = color;
        Handles.DrawAAPolyLine(thickness, p00, p10, p11, p01, p00);
    }

    private static bool TryGetMouseCell(Grid grid, Vector2 mousePosition, out Vector3Int cell)
    {
        var ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        var plane = new Plane(grid.transform.forward, grid.transform.position);
        if (plane.Raycast(ray, out var enter))
        {
            var world = ray.GetPoint(enter);
            cell = grid.WorldToCell(world);
            return true;
        }

        cell = default;
        return false;
    }

    private bool CanExecuteMove()
    {
        return _selection.size.x > 0
               && _selection.size.y > 0
               && _selection.size.z > 0
               && _moveOffset != Vector3Int.zero
               && GetEnabledTilemaps().Count > 0;
    }

    private bool ExecuteMove()
    {
        var enabledTilemaps = GetEnabledTilemaps();
        if (enabledTilemaps.Count == 0)
        {
            EditorUtility.DisplayDialog("Move Failed", "Enable at least one Tilemap.", "OK");
            return false;
        }

        if (_moveOffset == Vector3Int.zero)
        {
            EditorUtility.DisplayDialog("Move Failed", "Move Offset must not be (0,0,0).", "OK");
            return false;
        }

        if (_selection.size.x <= 0 || _selection.size.y <= 0 || _selection.size.z <= 0)
        {
            EditorUtility.DisplayDialog("Move Failed", "Selected Bounds must have a positive size.", "OK");
            return false;
        }

        var referenceGrid = ResolveReferenceGrid(enabledTilemaps);
        if (referenceGrid == null)
        {
            EditorUtility.DisplayDialog("Move Failed", "Could not resolve Grid for selected Tilemaps.", "OK");
            return false;
        }

        Undo.IncrementCurrentGroup();
        Undo.SetCurrentGroupName("Move Multi Tilemap Selection");
        var undoGroup = Undo.GetCurrentGroup();

        var touchedScenes = new HashSet<Scene>();
        var movedTiles = 0;
        for (var i = 0; i < enabledTilemaps.Count; i++)
        {
            var tilemap = enabledTilemaps[i];
            movedTiles += MoveTilesOnTilemap(referenceGrid, tilemap, _selection, _moveOffset);

            var scene = tilemap.gameObject.scene;
            if (scene.IsValid())
            {
                touchedScenes.Add(scene);
            }
        }

        var worldOffset = referenceGrid.CellToWorld(_moveOffset) - referenceGrid.CellToWorld(Vector3Int.zero);
        var movedObjects = _moveObjects ? MoveObjects(referenceGrid, worldOffset, enabledTilemaps) : 0;

        foreach (var scene in touchedScenes)
        {
            EditorSceneManager.MarkSceneDirty(scene);
        }

        Undo.CollapseUndoOperations(undoGroup);
        _selection.position += _moveOffset;
        TryUpdateGridSelectionPosition(_selection);

        Debug.Log(
            $"[MultiTilemapAreaMover] Moved tiles: {movedTiles}, moved objects: {movedObjects}, offset: {_moveOffset}.",
            referenceGrid);

        return movedTiles > 0 || movedObjects > 0;
    }

    private int MoveTilesOnTilemap(Grid referenceGrid, Tilemap tilemap, BoundsInt selection, Vector3Int moveOffset)
    {
        var snapshots = new List<TileSnapshot>();
        var sourceCells = new HashSet<Vector3Int>();

        foreach (var refCell in EnumerateCells(selection))
        {
            var world = referenceGrid.GetCellCenterWorld(refCell);
            var sourceCell = tilemap.WorldToCell(world);
            if (!tilemap.HasTile(sourceCell))
            {
                continue;
            }

            var tile = tilemap.GetTile(sourceCell);
            if (tile == null)
            {
                continue;
            }

            var destinationRefCell = refCell + moveOffset;
            var destinationWorld = referenceGrid.GetCellCenterWorld(destinationRefCell);
            var destinationCell = tilemap.WorldToCell(destinationWorld);

            snapshots.Add(new TileSnapshot
            {
                SourceCell = sourceCell,
                DestinationCell = destinationCell,
                Tile = tile,
                Flags = tilemap.GetTileFlags(sourceCell),
                Color = tilemap.GetColor(sourceCell),
                Transform = tilemap.GetTransformMatrix(sourceCell)
            });

            sourceCells.Add(sourceCell);
        }

        if (snapshots.Count == 0)
        {
            return 0;
        }

        Undo.RecordObject(tilemap, $"Move tiles on {tilemap.name}");

        foreach (var sourceCell in sourceCells)
        {
            tilemap.SetTile(sourceCell, null);
        }

        for (var i = 0; i < snapshots.Count; i++)
        {
            var snapshot = snapshots[i];
            tilemap.SetTile(snapshot.DestinationCell, snapshot.Tile);
            tilemap.SetTileFlags(snapshot.DestinationCell, TileFlags.None);
            tilemap.SetColor(snapshot.DestinationCell, snapshot.Color);
            tilemap.SetTransformMatrix(snapshot.DestinationCell, snapshot.Transform);
            tilemap.SetTileFlags(snapshot.DestinationCell, snapshot.Flags);
        }

        tilemap.CompressBounds();
        return snapshots.Count;
    }

    private int MoveObjects(Grid referenceGrid, Vector3 worldOffset, List<Tilemap> enabledTilemaps)
    {
        if (_objectsRoot == null)
        {
            return 0;
        }

        var selectionRect = BuildSelectionRect(referenceGrid, _selection);
        var tilemapRoots = BuildTilemapTransformSet(enabledTilemaps);
        var candidates = _objectsRoot.GetComponentsInChildren<Transform>(_includeInactiveObjects);

        var selected = new HashSet<Transform>();
        for (var i = 0; i < candidates.Length; i++)
        {
            var candidate = candidates[i];
            if (!CanMoveObject(candidate, tilemapRoots))
            {
                continue;
            }

            if (!IsTransformInsideSelection(candidate, selectionRect))
            {
                continue;
            }

            selected.Add(candidate);
        }

        if (selected.Count == 0)
        {
            return 0;
        }

        var rootsToMove = FilterTopMostTransforms(selected);
        for (var i = 0; i < rootsToMove.Count; i++)
        {
            var item = rootsToMove[i];
            Undo.RecordObject(item, $"Move object {item.name}");
            item.position += worldOffset;

            var scene = item.gameObject.scene;
            if (scene.IsValid())
            {
                EditorSceneManager.MarkSceneDirty(scene);
            }
        }

        return rootsToMove.Count;
    }

    private bool CanMoveObject(Transform candidate, HashSet<Transform> tilemapRoots)
    {
        if (candidate == null || candidate == _objectsRoot)
        {
            return false;
        }

        if (candidate.GetComponent<Grid>() != null)
        {
            return false;
        }

        if (candidate.GetComponentInParent<Tilemap>() != null)
        {
            return false;
        }

        foreach (var tilemapRoot in tilemapRoots)
        {
            if (tilemapRoot != null && candidate.IsChildOf(tilemapRoot))
            {
                return false;
            }
        }

        var componentCount = candidate.GetComponents<Component>().Length;
        if (componentCount <= 1)
        {
            return false;
        }

        return true;
    }

    private bool IsTransformInsideSelection(Transform candidate, Rect selectionRect)
    {
        if (_useBoundsForObjects && TryGetCompositeBounds(candidate, out var bounds))
        {
            return BoundsOverlapsRect(bounds, selectionRect);
        }

        var pos = candidate.position;
        return pos.x >= selectionRect.xMin
               && pos.x <= selectionRect.xMax
               && pos.y >= selectionRect.yMin
               && pos.y <= selectionRect.yMax;
    }

    private bool TryGetCompositeBounds(Transform root, out Bounds bounds)
    {
        var found = false;
        bounds = default;

        var renderer = root.GetComponent<Renderer>();
        if (renderer != null && !(renderer is TilemapRenderer))
        {
            bounds = renderer.bounds;
            found = true;
        }

        var collider2D = root.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            if (!found)
            {
                bounds = collider2D.bounds;
                found = true;
            }
            else
            {
                bounds.Encapsulate(collider2D.bounds);
            }
        }

        var collider3D = root.GetComponent<Collider>();
        if (collider3D != null)
        {
            if (!found)
            {
                bounds = collider3D.bounds;
                found = true;
            }
            else
            {
                bounds.Encapsulate(collider3D.bounds);
            }
        }

        return found;
    }

    private static bool BoundsOverlapsRect(Bounds bounds, Rect rect)
    {
        if (bounds.max.x < rect.xMin || bounds.min.x > rect.xMax)
        {
            return false;
        }

        if (bounds.max.y < rect.yMin || bounds.min.y > rect.yMax)
        {
            return false;
        }

        return true;
    }

    private static Rect BuildSelectionRect(Grid referenceGrid, BoundsInt selection)
    {
        var worldA = referenceGrid.CellToWorld(selection.min);
        var worldB = referenceGrid.CellToWorld(selection.max);

        var xMin = Mathf.Min(worldA.x, worldB.x);
        var xMax = Mathf.Max(worldA.x, worldB.x);
        var yMin = Mathf.Min(worldA.y, worldB.y);
        var yMax = Mathf.Max(worldA.y, worldB.y);

        return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
    }

    private static List<Transform> FilterTopMostTransforms(HashSet<Transform> selected)
    {
        var result = new List<Transform>();
        foreach (var item in selected)
        {
            var hasSelectedParent = false;
            var parent = item.parent;
            while (parent != null)
            {
                if (selected.Contains(parent))
                {
                    hasSelectedParent = true;
                    break;
                }

                parent = parent.parent;
            }

            if (!hasSelectedParent)
            {
                result.Add(item);
            }
        }

        return result;
    }

    private static HashSet<Transform> BuildTilemapTransformSet(List<Tilemap> tilemaps)
    {
        var set = new HashSet<Transform>();
        for (var i = 0; i < tilemaps.Count; i++)
        {
            var tilemap = tilemaps[i];
            if (tilemap != null)
            {
                set.Add(tilemap.transform);
            }
        }

        return set;
    }

    private static void TryUpdateGridSelectionPosition(BoundsInt bounds)
    {
        if (!GridSelection.active)
        {
            return;
        }

        GridSelection.position = bounds;
    }

    private Grid ResolveReferenceGrid(List<Tilemap> enabledTilemaps)
    {
        if (GridSelection.active && GridSelection.grid != null)
        {
            return GridSelection.grid;
        }

        for (var i = 0; i < enabledTilemaps.Count; i++)
        {
            var tilemap = enabledTilemaps[i];
            if (tilemap == null)
            {
                continue;
            }

            var grid = tilemap.GetComponentInParent<Grid>();
            if (grid != null)
            {
                return grid;
            }
        }

        return _tilemapsRoot != null ? _tilemapsRoot.GetComponentInParent<Grid>() : null;
    }

    private static IEnumerable<Vector3Int> EnumerateCells(BoundsInt bounds)
    {
        for (var z = bounds.zMin; z < bounds.zMax; z++)
        {
            for (var y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (var x = bounds.xMin; x < bounds.xMax; x++)
                {
                    yield return new Vector3Int(x, y, z);
                }
            }
        }
    }

    private string BuildTilemapLabel(Tilemap tilemap)
    {
        if (tilemap == null)
        {
            return "(Missing)";
        }

        var path = BuildRelativePath(tilemap.transform, _tilemapsRoot);
        return string.IsNullOrEmpty(path) ? tilemap.name : $"{tilemap.name}  [{path}]";
    }

    private static string BuildRelativePath(Transform target, Transform root)
    {
        if (target == null || root == null)
        {
            return string.Empty;
        }

        if (target == root)
        {
            return root.name;
        }

        if (!target.IsChildOf(root))
        {
            return target.name;
        }

        var names = new List<string>();
        var current = target;
        while (current != null && current != root)
        {
            names.Add(current.name);
            current = current.parent;
        }

        names.Reverse();
        return string.Join("/", names);
    }

    private List<Tilemap> GetEnabledTilemaps()
    {
        var result = new List<Tilemap>();
        for (var i = 0; i < _tilemaps.Count; i++)
        {
            var entry = _tilemaps[i];
            if (entry == null || !entry.enabled || entry.tilemap == null)
            {
                continue;
            }

            result.Add(entry.tilemap);
        }

        return result;
    }

    private void SetAllTilemapsEnabled(bool value)
    {
        for (var i = 0; i < _tilemaps.Count; i++)
        {
            var entry = _tilemaps[i];
            if (entry == null)
            {
                continue;
            }

            entry.enabled = value;
        }
    }

    private bool CaptureSelectionFromGridSelection(bool showErrors)
    {
        if (!GridSelection.active || GridSelection.grid == null)
        {
            if (showErrors)
            {
                EditorUtility.DisplayDialog(
                    "No Grid Selection",
                    "Create a Tilemap selection first using the Select Tool.",
                    "OK");
            }

            return false;
        }

        _selection = GridSelection.position;
        if (_selection.size.z <= 0)
        {
            _selection.size = new Vector3Int(_selection.size.x, _selection.size.y, 1);
        }

        _tilemapsRoot = GridSelection.grid.transform;
        if (_objectsRoot == null || _objectsRoot == _tilemapsRoot)
        {
            _objectsRoot = _tilemapsRoot.parent != null ? _tilemapsRoot.parent : _tilemapsRoot;
        }

        RefreshTilemaps();
        return true;
    }

    private void AutoResolveRoots()
    {
        if (_tilemapsRoot != null)
        {
            return;
        }

        if (GridSelection.active && GridSelection.grid != null)
        {
            _tilemapsRoot = GridSelection.grid.transform;
        }

        if (_tilemapsRoot == null && Selection.activeTransform != null)
        {
            var selectedTilemap = Selection.activeTransform.GetComponent<Tilemap>();
            if (selectedTilemap != null)
            {
                var grid = selectedTilemap.GetComponentInParent<Grid>();
                if (grid != null)
                {
                    _tilemapsRoot = grid.transform;
                }
            }
            else
            {
                var tilemapInChildren = Selection.activeTransform.GetComponentInChildren<Tilemap>(true);
                if (tilemapInChildren != null)
                {
                    var grid = tilemapInChildren.GetComponentInParent<Grid>();
                    if (grid != null)
                    {
                        _tilemapsRoot = grid.transform;
                    }
                }
            }
        }

        if (_tilemapsRoot == null)
        {
            var worldTilemaps = GameObject.Find("WorldTilemaps");
            if (worldTilemaps != null)
            {
                _tilemapsRoot = worldTilemaps.transform;
            }
        }

        if (_objectsRoot == null && _tilemapsRoot != null)
        {
            _objectsRoot = _tilemapsRoot.parent != null ? _tilemapsRoot.parent : _tilemapsRoot;
        }
    }

    private void RefreshTilemaps()
    {
        var previousState = new Dictionary<Tilemap, bool>();
        for (var i = 0; i < _tilemaps.Count; i++)
        {
            var entry = _tilemaps[i];
            if (entry == null || entry.tilemap == null)
            {
                continue;
            }

            previousState[entry.tilemap] = entry.enabled;
        }

        _tilemaps.Clear();
        if (_tilemapsRoot == null)
        {
            return;
        }

        var found = _tilemapsRoot.GetComponentsInChildren<Tilemap>(true);
        for (var i = 0; i < found.Length; i++)
        {
            var tilemap = found[i];
            if (tilemap == null)
            {
                continue;
            }

            var entry = new TilemapEntry { tilemap = tilemap, enabled = true };
            if (previousState.TryGetValue(tilemap, out var wasEnabled))
            {
                entry.enabled = wasEnabled;
            }

            _tilemaps.Add(entry);
        }
    }
}

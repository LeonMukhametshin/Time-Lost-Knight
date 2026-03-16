using Game.Core.Infrastructure.States;
using Game.Entities;
using Game.UI.PopupWindow;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Editor
{
    [MovedFrom("")]
    public sealed class EnemySpawnerPainterWindow : EditorWindow
    {
        private const string MenuPath = "Tools/Level/Enemy Spawner Painter";

        [SerializeField] private EnemySpawner m_spawnerPrefab;
        [SerializeField] private Transform m_parentRoot;
        [SerializeField] private int m_selectedComboIndex;
        [SerializeField] private bool m_placementMode;
        [SerializeField] private bool m_splitPreview = true;
        [SerializeField][Range(0.1f, 1f)] private float m_previewAlpha = 0.35f;
        [SerializeField][Min(0.1f)] private float m_previewSpacing = 0.85f;
        [SerializeField] private bool m_showPlacedSpawnPreviews = true;
        [SerializeField] private bool m_showPreviewLabels = true;

        private readonly List<Entity> m_availableEnemies = new List<Entity>();
        private readonly List<List<Entity>> m_enemyCombos = new List<List<Entity>>();
        private readonly List<string> m_comboLabels = new List<string>();
        private readonly List<Entity> m_bufferEnemies = new List<Entity>();
        private readonly List<Transform> m_bufferSpawnPoints = new List<Transform>();
        private readonly List<int> m_stalePreviewKeys = new List<int>();
        private readonly Dictionary<int, GameObject> m_spawnPointPreviews = new Dictionary<int, GameObject>();
        private readonly Dictionary<int, int> m_spawnPointPreviewHashes = new Dictionary<int, int>();

        private GameObject m_previewRoot;
        private GameObject m_spawnPointsPreviewRoot;
        private int m_previewComboIndex = -1;
        private Vector3 m_previewWorldPosition;
        private Vector2 m_scroll;

        [MenuItem(MenuPath)]
        private static void Open()
        {
            GetWindow<EnemySpawnerPainterWindow>("Enemy Painter");
        }

        private void OnEnable()
        {
            if (m_spawnerPrefab == null)
                AutoResolveSpawnerPrefab();

            RebuildCombosFromPrefab();
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            DestroyPreview();
            DestroyPlacedSpawnPreviews();
            m_placementMode = false;
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(
                "Click placement adds Spawnpoint to an existing combo spawner. If it does not exist, it is created once.",
                MessageType.Info);

            DrawSetupPanel();
            EditorGUILayout.Space(8f);
            DrawComboPanel();
            EditorGUILayout.Space(8f);
            DrawPlacementPanel();
        }

        private void DrawSetupPanel()
        {
            EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            m_spawnerPrefab = (EnemySpawner)EditorGUILayout.ObjectField(
                "Spawner Prefab",
                m_spawnerPrefab,
                typeof(EnemySpawner),
                false);
            if (EditorGUI.EndChangeCheck())
            {
                RebuildCombosFromPrefab();
                DestroyPreview();
                DestroyPlacedSpawnPreviews();
            }

            m_parentRoot = (Transform)EditorGUILayout.ObjectField(
                "Parent Root (Optional)",
                m_parentRoot,
                typeof(Transform),
                true);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Find Default Prefab"))
                {
                    AutoResolveSpawnerPrefab();
                    RebuildCombosFromPrefab();
                    DestroyPreview();
                    DestroyPlacedSpawnPreviews();
                }

                if (GUILayout.Button("Refresh Enemy Combos"))
                {
                    RebuildCombosFromPrefab();
                    DestroyPreview();
                    DestroyPlacedSpawnPreviews();
                }
            }
        }

        private void DrawComboPanel()
        {
            EditorGUILayout.LabelField("Spawner Type", EditorStyles.boldLabel);

            if (m_comboLabels.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "Failed to build combo list. Check m_enemies in EnemySpawner prefab.",
                    MessageType.Warning);
                return;
            }

            m_selectedComboIndex = Mathf.Clamp(m_selectedComboIndex, 0, m_comboLabels.Count - 1);
            var labels = m_comboLabels.ToArray();
            var newIndex = EditorGUILayout.Popup("Combo", m_selectedComboIndex, labels);
            if (newIndex != m_selectedComboIndex)
            {
                m_selectedComboIndex = newIndex;
                DestroyPreview();
            }

            m_scroll = EditorGUILayout.BeginScrollView(m_scroll, GUILayout.MaxHeight(120f));
            for (var i = 0; i < m_comboLabels.Count; i++)
                EditorGUILayout.LabelField($"{i + 1}. {m_comboLabels[i]}");
            EditorGUILayout.EndScrollView();
        }

        private void DrawPlacementPanel()
        {
            EditorGUILayout.LabelField("Placement", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            m_splitPreview = EditorGUILayout.Toggle("Split Preview For Combo", m_splitPreview);
            m_previewAlpha = EditorGUILayout.Slider("Preview Alpha", m_previewAlpha, 0.1f, 1f);
            m_previewSpacing = EditorGUILayout.FloatField("Preview Spacing", m_previewSpacing);
            m_previewSpacing = Mathf.Max(0.1f, m_previewSpacing);
            m_showPlacedSpawnPreviews = EditorGUILayout.Toggle("Show Existing Spawn Previews", m_showPlacedSpawnPreviews);
            m_showPreviewLabels = EditorGUILayout.Toggle("Show Preview Labels", m_showPreviewLabels);
            if (EditorGUI.EndChangeCheck())
            {
                DestroyPreview();
                DestroyPlacedSpawnPreviews();
            }

            var buttonLabel = m_placementMode ? "Stop Placement (Esc)" : "Start Placement";
            if (GUILayout.Button(buttonLabel, GUILayout.Height(28f)))
            {
                m_placementMode = !m_placementMode;
                if (!m_placementMode)
                    DestroyPreview();
            }

            if (m_placementMode)
            {
                EditorGUILayout.HelpBox(
                    "LMB: add spawnpoint to selected combo spawner. Esc: stop placement.",
                    MessageType.None);
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (m_showPlacedSpawnPreviews)
                EnsurePlacedSpawnPreviews();
            else
                DestroyPlacedSpawnPreviews();

            if (!m_placementMode)
                return;

            var evt = Event.current;
            if (evt == null)
                return;

            if (evt.type == EventType.KeyDown && evt.keyCode == KeyCode.Escape)
            {
                m_placementMode = false;
                DestroyPreview();
                Repaint();
                evt.Use();
                return;
            }

            if (!TryGetMouseWorldPosition(evt.mousePosition, out var worldPosition))
                return;

            m_previewWorldPosition = worldPosition;
            EnsurePreview();
            UpdatePreviewTransform();

            Handles.color = new Color(0.15f, 1f, 0.4f, 0.85f);
            Handles.DrawWireDisc(worldPosition, Vector3.forward, 0.3f);

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (evt.type == EventType.MouseDown && evt.button == 0 && !evt.alt)
            {
                AddSpawnPointToCombo(m_selectedComboIndex, worldPosition);
                evt.Use();
            }

            sceneView.Repaint();
        }

        private void AddSpawnPointToCombo(int comboIndex, Vector3 worldPosition)
        {
            if (m_spawnerPrefab == null)
                return;

            if (!TryGetCombo(comboIndex, out var combo, out var comboLabel))
                return;

            var spawner = FindExistingSpawnerForCombo(comboLabel);
            if (spawner == null)
                spawner = CreateSpawnerForCombo(combo, comboLabel, worldPosition);

            if (spawner == null)
                return;

            var spawnPoint = CreateSpawnPoint(spawner.transform, worldPosition);
            if (spawnPoint == null)
                return;

            UpdateSpawnerData(spawner, combo, spawnPoint, clearExistingPoints: false);

            Selection.activeGameObject = spawnPoint.gameObject;
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }

        private EnemySpawner FindExistingSpawnerForCombo(string comboLabel)
        {
            var expectedName = BuildSpawnerName(comboLabel);
            var spawners = Object.FindObjectsByType<EnemySpawner>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None);

            for (var i = 0; i < spawners.Length; i++)
            {
                var spawner = spawners[i];
                if (spawner == null)
                    continue;

                if (m_parentRoot != null && !spawner.transform.IsChildOf(m_parentRoot))
                    continue;

                if (spawner.name == expectedName)
                    return spawner;
            }

            return null;
        }

        private EnemySpawner CreateSpawnerForCombo(IReadOnlyList<Entity> combo, string comboLabel, Vector3 worldPosition)
        {
            var prefabObject = m_spawnerPrefab.gameObject;
            var instance = PrefabUtility.InstantiatePrefab(prefabObject) as GameObject;
            if (instance == null)
                return null;

            Undo.RegisterCreatedObjectUndo(instance, "Create Enemy Spawner");

            if (m_parentRoot != null)
                Undo.SetTransformParent(instance.transform, m_parentRoot, "Set Enemy Spawner Parent");

            instance.transform.position = worldPosition;
            instance.transform.rotation = Quaternion.identity;
            instance.name = BuildSpawnerName(comboLabel);

            var spawner = instance.GetComponent<EnemySpawner>();
            if (spawner == null)
            {
                Debug.LogError("EnemySpawner component missing on prefab instance.");
                return null;
            }

            RemoveAllChildren(instance.transform);
            UpdateSpawnerData(spawner, combo, null, clearExistingPoints: true);

            return spawner;
        }

        private static void RemoveAllChildren(Transform root)
        {
            while (root.childCount > 0)
                Undo.DestroyObjectImmediate(root.GetChild(0).gameObject);
        }

        private static Transform CreateSpawnPoint(Transform spawnerRoot, Vector3 worldPosition)
        {
            var nextIndex = spawnerRoot.childCount + 1;
            var spawnPointObject = new GameObject($"Spawnpoint ({nextIndex})");
            Undo.RegisterCreatedObjectUndo(spawnPointObject, "Create Enemy Spawnpoint");

            var spawnPoint = spawnPointObject.transform;
            Undo.SetTransformParent(spawnPoint, spawnerRoot, "Set Spawnpoint Parent");

            spawnPoint.position = worldPosition;
            spawnPoint.rotation = Quaternion.identity;
            spawnPoint.localScale = Vector3.one;

            return spawnPoint;
        }

        private static void UpdateSpawnerData(EnemySpawner spawner, IReadOnlyList<Entity> combo, Transform newSpawnPoint, bool clearExistingPoints)
        {
            var serializedObject = new SerializedObject(spawner);

            var enemiesProperty = serializedObject.FindProperty("m_enemies");
            enemiesProperty.arraySize = combo.Count;
            for (var i = 0; i < combo.Count; i++)
                enemiesProperty.GetArrayElementAtIndex(i).objectReferenceValue = combo[i];

            var pointsProperty = serializedObject.FindProperty("m_spawnPoints");
            var points = new List<Transform>();
            if (!clearExistingPoints)
            {
                for (var i = 0; i < pointsProperty.arraySize; i++)
                {
                    var point = pointsProperty.GetArrayElementAtIndex(i).objectReferenceValue as Transform;
                    if (point != null)
                        points.Add(point);
                }
            }

            if (newSpawnPoint != null)
                points.Add(newSpawnPoint);

            pointsProperty.arraySize = points.Count;
            for (var i = 0; i < points.Count; i++)
                pointsProperty.GetArrayElementAtIndex(i).objectReferenceValue = points[i];

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private static string BuildSpawnerName(string comboLabel)
        {
            return $"EnemySpawner [{comboLabel}]";
        }

        private void EnsurePreview()
        {
            if (!TryGetCombo(m_selectedComboIndex, out var combo, out _))
            {
                DestroyPreview();
                return;
            }

            if (m_previewRoot != null && m_previewComboIndex == m_selectedComboIndex)
                return;

            DestroyPreview();

            m_previewRoot = new GameObject("__EnemySpawnerPreview__");
            m_previewRoot.hideFlags = HideFlags.HideAndDontSave;
            m_previewComboIndex = m_selectedComboIndex;

            var count = combo.Count;
            for (var i = 0; i < count; i++)
            {
                var enemy = combo[i];
                if (enemy == null)
                    continue;

                var previewInstance = PrefabUtility.InstantiatePrefab(enemy.gameObject) as GameObject;
                if (previewInstance == null)
                    continue;

                previewInstance.name = $"Preview_{enemy.name}";
                previewInstance.transform.SetParent(m_previewRoot.transform, false);
                previewInstance.hideFlags = HideFlags.HideAndDontSave;
                ApplyHideFlagsToChildren(previewInstance.transform);
                MakePreviewVisual(previewInstance, i, count);
            }
        }

        private void UpdatePreviewTransform()
        {
            if (m_previewRoot == null)
                return;

            m_previewRoot.transform.position = m_previewWorldPosition;
            m_previewRoot.transform.rotation = Quaternion.identity;
        }

        private void MakePreviewVisual(GameObject previewInstance, int comboIndex, int comboCount)
        {
            var behaviours = previewInstance.GetComponentsInChildren<Behaviour>(true);
            for (var i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] is SpriteRenderer)
                    continue;

                behaviours[i].enabled = false;
            }

            var rigidbodies = previewInstance.GetComponentsInChildren<Rigidbody2D>(true);
            for (var i = 0; i < rigidbodies.Length; i++)
                rigidbodies[i].simulated = false;

            var spriteRenderers = previewInstance.GetComponentsInChildren<SpriteRenderer>(true);
            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                var color = spriteRenderers[i].color;
                color.a = m_previewAlpha;
                spriteRenderers[i].color = color;
                spriteRenderers[i].sortingOrder += 1000;
            }

            if (!m_splitPreview || comboCount <= 1)
                return;

            var scale = previewInstance.transform.localScale;
            scale.x *= 1f / comboCount;
            previewInstance.transform.localScale = scale;

            var xOffset = (comboIndex - ((comboCount - 1) * 0.5f)) * m_previewSpacing;
            var localPosition = previewInstance.transform.localPosition;
            localPosition.x = xOffset;
            previewInstance.transform.localPosition = localPosition;
        }

        private void EnsurePlacedSpawnPreviews()
        {
            EnsureSpawnPointsPreviewRoot();

            var activePreviewIds = new HashSet<int>();
            var spawners = Object.FindObjectsByType<EnemySpawner>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None);

            for (var i = 0; i < spawners.Length; i++)
            {
                var spawner = spawners[i];
                if (spawner == null)
                    continue;

                if (m_parentRoot != null && !spawner.transform.IsChildOf(m_parentRoot))
                    continue;

                ReadSpawnerData(spawner, m_bufferEnemies, m_bufferSpawnPoints);
                if (m_bufferEnemies.Count == 0 || m_bufferSpawnPoints.Count == 0)
                    continue;

                var comboHash = BuildPreviewHash(m_bufferEnemies);
                var comboLabel = BuildComboLabel(m_bufferEnemies);

                for (var pointIndex = 0; pointIndex < m_bufferSpawnPoints.Count; pointIndex++)
                {
                    var spawnPoint = m_bufferSpawnPoints[pointIndex];
                    if (spawnPoint == null)
                        continue;

                    var pointKey = spawnPoint.GetInstanceID();
                    activePreviewIds.Add(pointKey);

                    if (!m_spawnPointPreviews.TryGetValue(pointKey, out var previewRoot)
                        || previewRoot == null
                        || !m_spawnPointPreviewHashes.TryGetValue(pointKey, out var oldHash)
                        || oldHash != comboHash)
                    {
                        if (previewRoot != null)
                            DestroyImmediate(previewRoot);

                        previewRoot = CreateComboPreviewRoot(m_bufferEnemies, $"__EnemySpawnPreview_{pointKey}");
                        m_spawnPointPreviews[pointKey] = previewRoot;
                        m_spawnPointPreviewHashes[pointKey] = comboHash;
                    }

                    if (previewRoot != null)
                    {
                        previewRoot.transform.position = spawnPoint.position;
                        previewRoot.transform.rotation = Quaternion.identity;
                    }

                    if (m_showPreviewLabels)
                    {
                        Handles.color = new Color(1f, 1f, 1f, 0.9f);
                        Handles.Label(spawnPoint.position + Vector3.up * 1.35f, comboLabel);
                    }
                }
            }

            m_stalePreviewKeys.Clear();
            foreach (var pair in m_spawnPointPreviews)
            {
                if (!activePreviewIds.Contains(pair.Key))
                {
                    if (pair.Value != null)
                        DestroyImmediate(pair.Value);
                    m_stalePreviewKeys.Add(pair.Key);
                }
            }

            for (var i = 0; i < m_stalePreviewKeys.Count; i++)
            {
                var key = m_stalePreviewKeys[i];
                m_spawnPointPreviews.Remove(key);
                m_spawnPointPreviewHashes.Remove(key);
            }
        }

        private GameObject CreateComboPreviewRoot(IReadOnlyList<Entity> combo, string name)
        {
            var root = new GameObject(name);
            root.hideFlags = HideFlags.HideAndDontSave;
            ApplyHideFlagsToChildren(root.transform);

            if (m_spawnPointsPreviewRoot != null)
                root.transform.SetParent(m_spawnPointsPreviewRoot.transform, false);

            var count = combo.Count;
            for (var i = 0; i < count; i++)
            {
                var enemy = combo[i];
                if (enemy == null)
                    continue;

                var previewInstance = PrefabUtility.InstantiatePrefab(enemy.gameObject) as GameObject;
                if (previewInstance == null)
                    continue;

                previewInstance.name = $"Preview_{enemy.name}";
                previewInstance.transform.SetParent(root.transform, false);
                previewInstance.hideFlags = HideFlags.HideAndDontSave;
                ApplyHideFlagsToChildren(previewInstance.transform);
                MakePreviewVisual(previewInstance, i, count);
            }

            return root;
        }

        private void EnsureSpawnPointsPreviewRoot()
        {
            if (m_spawnPointsPreviewRoot != null)
                return;

            m_spawnPointsPreviewRoot = new GameObject("__EnemySpawnPointsPreview__");
            m_spawnPointsPreviewRoot.hideFlags = HideFlags.HideAndDontSave;
            ApplyHideFlagsToChildren(m_spawnPointsPreviewRoot.transform);
        }

        private void DestroyPlacedSpawnPreviews()
        {
            foreach (var pair in m_spawnPointPreviews)
            {
                if (pair.Value != null)
                    DestroyImmediate(pair.Value);
            }

            m_spawnPointPreviews.Clear();
            m_spawnPointPreviewHashes.Clear();
            m_stalePreviewKeys.Clear();

            if (m_spawnPointsPreviewRoot != null)
                DestroyImmediate(m_spawnPointsPreviewRoot);
            m_spawnPointsPreviewRoot = null;
        }

        private int BuildPreviewHash(IReadOnlyList<Entity> combo)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 31 + combo.Count;
                for (var i = 0; i < combo.Count; i++)
                    hash = hash * 31 + (combo[i] != null ? combo[i].GetInstanceID() : 0);

                hash = hash * 31 + (m_splitPreview ? 1 : 0);
                hash = hash * 31 + Mathf.RoundToInt(m_previewAlpha * 1000f);
                hash = hash * 31 + Mathf.RoundToInt(m_previewSpacing * 100f);
                return hash;
            }
        }

        private static void ReadSpawnerData(EnemySpawner spawner, List<Entity> enemies, List<Transform> spawnPoints)
        {
            enemies.Clear();
            spawnPoints.Clear();

            var serializedObject = new SerializedObject(spawner);

            var enemiesProperty = serializedObject.FindProperty("m_enemies");
            if (enemiesProperty != null && enemiesProperty.isArray)
            {
                for (var i = 0; i < enemiesProperty.arraySize; i++)
                {
                    var entity = enemiesProperty.GetArrayElementAtIndex(i).objectReferenceValue as Entity;
                    if (entity != null)
                        enemies.Add(entity);
                }
            }

            var pointsProperty = serializedObject.FindProperty("m_spawnPoints");
            if (pointsProperty != null && pointsProperty.isArray)
            {
                for (var i = 0; i < pointsProperty.arraySize; i++)
                {
                    var point = pointsProperty.GetArrayElementAtIndex(i).objectReferenceValue as Transform;
                    if (point != null)
                        spawnPoints.Add(point);
                }
            }
        }

        private void DestroyPreview()
        {
            if (m_previewRoot != null)
                DestroyImmediate(m_previewRoot);

            m_previewRoot = null;
            m_previewComboIndex = -1;
        }

        private void RebuildCombosFromPrefab()
        {
            m_availableEnemies.Clear();
            m_enemyCombos.Clear();
            m_comboLabels.Clear();

            if (m_spawnerPrefab == null)
                return;

            var serializedObject = new SerializedObject(m_spawnerPrefab);
            var enemiesProperty = serializedObject.FindProperty("m_enemies");
            if (enemiesProperty == null || !enemiesProperty.isArray)
                return;

            for (var i = 0; i < enemiesProperty.arraySize; i++)
            {
                var entity = enemiesProperty.GetArrayElementAtIndex(i).objectReferenceValue as Entity;
                if (entity == null || m_availableEnemies.Contains(entity))
                    continue;

                m_availableEnemies.Add(entity);
            }

            var count = m_availableEnemies.Count;
            if (count == 0)
                return;

            var maxMask = 1 << count;
            for (var mask = 1; mask < maxMask; mask++)
            {
                var combo = new List<Entity>();
                for (var bit = 0; bit < count; bit++)
                {
                    if ((mask & (1 << bit)) != 0)
                        combo.Add(m_availableEnemies[bit]);
                }

                m_enemyCombos.Add(combo);
                m_comboLabels.Add(BuildComboLabel(combo));
            }

            m_selectedComboIndex = Mathf.Clamp(m_selectedComboIndex, 0, Mathf.Max(0, m_comboLabels.Count - 1));
        }

        private bool TryGetCombo(int index, out List<Entity> combo, out string label)
        {
            combo = null;
            label = string.Empty;

            if (index < 0 || index >= m_enemyCombos.Count)
                return false;

            combo = m_enemyCombos[index];
            label = m_comboLabels[index];
            return true;
        }

        private static string BuildComboLabel(IReadOnlyList<Entity> combo)
        {
            if (combo == null || combo.Count == 0)
                return "Empty";

            var label = combo[0] != null ? combo[0].name : "None";
            for (var i = 1; i < combo.Count; i++)
            {
                var name = combo[i] != null ? combo[i].name : "None";
                label += "+" + name;
            }

            return label;
        }

        private void AutoResolveSpawnerPrefab()
        {
            var guids = AssetDatabase.FindAssets(
                "t:Prefab EnemySpawner",
                new[] { "Assets/_Time-Lost-Knight/Prefabs/Enemy" });

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null)
                    continue;

                var spawner = prefab.GetComponent<EnemySpawner>();
                if (spawner != null)
                {
                    m_spawnerPrefab = spawner;
                    return;
                }
            }
        }

        private static bool TryGetMouseWorldPosition(Vector2 guiMousePosition, out Vector3 worldPosition)
        {
            var ray = HandleUtility.GUIPointToWorldRay(guiMousePosition);
            if (Mathf.Abs(ray.direction.z) < 0.0001f)
            {
                worldPosition = Vector3.zero;
                return false;
            }

            var distance = -ray.origin.z / ray.direction.z;
            if (distance < 0f)
            {
                worldPosition = Vector3.zero;
                return false;
            }

            worldPosition = ray.origin + ray.direction * distance;
            worldPosition.z = 0f;
            return true;
        }

        private static void ApplyHideFlagsToChildren(Transform root)
        {
            root.gameObject.hideFlags = HideFlags.HideAndDontSave;
            root.hideFlags = HideFlags.HideAndDontSave;
            for (var i = 0; i < root.childCount; i++)
                ApplyHideFlagsToChildren(root.GetChild(i));
        }
    }
}

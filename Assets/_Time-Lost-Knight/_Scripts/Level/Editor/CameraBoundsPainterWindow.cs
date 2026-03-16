using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Editor
{
    [MovedFrom("")]
    public sealed class CameraBoundsPainterWindow : EditorWindow
    {
        private const string MenuPath = "Tools/Level/Camera Bounds/Painter";

        private enum BrushMode
        {
            Add,
            Remove,
            Toggle,
        }

        [SerializeField] private CompositeCollider2D m_source;
        [SerializeField] private CinemachineConfiner2D m_confiner;
        [SerializeField] private BrushMode m_brushMode = BrushMode.Add;
        [SerializeField] private bool m_paintMode;
        [SerializeField] private bool m_showLabels = true;
        [SerializeField][Range(1f, 6f)] private float m_lineWidth = 2f;

        private readonly List<CameraBoundsGenerator.PathData> m_paths = new List<CameraBoundsGenerator.PathData>();
        private readonly HashSet<int> m_selectedPathIndices = new HashSet<int>();

        private Vector2 m_listScroll;
        private int m_hoverPathIndex = -1;
        private Vector3 m_hoverWorldPosition;

        [MenuItem(MenuPath)]
        private static void OpenFromMenu()
        {
            OpenWindow();
        }

        internal static void OpenWindow()
        {
            GetWindow<CameraBoundsPainterWindow>("Camera Bounds");
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
            AutoResolveReferences();
            RebuildPaths(resetSelectionToLargest: true);
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            m_paintMode = false;
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(
                "Base flow: generate largest outer contour first, then paint additional areas from Bounds paths.",
                MessageType.Info);

            DrawSetupPanel();
            EditorGUILayout.Space(6f);
            DrawSelectionPanel();
            EditorGUILayout.Space(6f);
            DrawPaintPanel();
            EditorGUILayout.Space(6f);
            DrawApplyPanel();
        }

        private void DrawSetupPanel()
        {
            EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            m_source = (CompositeCollider2D)EditorGUILayout.ObjectField(
                "Bounds Composite",
                m_source,
                typeof(CompositeCollider2D),
                true);
            m_confiner = (CinemachineConfiner2D)EditorGUILayout.ObjectField(
                "Cinemachine Confiner2D",
                m_confiner,
                typeof(CinemachineConfiner2D),
                true);
            if (EditorGUI.EndChangeCheck())
            {
                RebuildPaths(resetSelectionToLargest: true);
                Repaint();
                SceneView.RepaintAll();
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Auto Resolve"))
                {
                    AutoResolveReferences();
                    RebuildPaths(resetSelectionToLargest: true);
                    Repaint();
                    SceneView.RepaintAll();
                }

                if (GUILayout.Button("Refresh Paths"))
                {
                    RebuildPaths(resetSelectionToLargest: false);
                    Repaint();
                    SceneView.RepaintAll();
                }
            }

            EditorGUILayout.LabelField($"Found Paths: {m_paths.Count}");
            EditorGUILayout.LabelField($"Selected Paths: {m_selectedPathIndices.Count}");
        }

        private void DrawSelectionPanel()
        {
            EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Reset To Largest"))
                {
                    SelectLargestOnly();
                    Repaint();
                    SceneView.RepaintAll();
                }

                if (GUILayout.Button("Select All"))
                {
                    SelectAll();
                    Repaint();
                    SceneView.RepaintAll();
                }

                if (GUILayout.Button("Clear"))
                {
                    m_selectedPathIndices.Clear();
                    Repaint();
                    SceneView.RepaintAll();
                }
            }

            m_listScroll = EditorGUILayout.BeginScrollView(m_listScroll, GUILayout.MaxHeight(180f));
            for (var i = 0; i < m_paths.Count; i++)
            {
                var path = m_paths[i];
                var selected = m_selectedPathIndices.Contains(i);
                var label = $"Path {i + 1} (src {path.SourcePathIndex})  area: {path.AbsArea:F1}";
                var updated = EditorGUILayout.ToggleLeft(label, selected);
                if (updated != selected)
                {
                    if (updated)
                        m_selectedPathIndices.Add(i);
                    else
                        m_selectedPathIndices.Remove(i);

                    SceneView.RepaintAll();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawPaintPanel()
        {
            EditorGUILayout.LabelField("Paint", EditorStyles.boldLabel);

            m_brushMode = (BrushMode)EditorGUILayout.EnumPopup("Brush Mode", m_brushMode);
            m_showLabels = EditorGUILayout.Toggle("Show Path Labels", m_showLabels);
            m_lineWidth = EditorGUILayout.Slider("Line Width", m_lineWidth, 1f, 6f);

            var buttonLabel = m_paintMode ? "Stop Paint (Esc)" : "Start Paint";
            if (GUILayout.Button(buttonLabel, GUILayout.Height(28f)))
            {
                m_paintMode = !m_paintMode;
                SceneView.RepaintAll();
            }

            if (m_paintMode)
            {
                EditorGUILayout.HelpBox(
                    "LMB on contour area: apply Add/Remove/Toggle. Esc: stop paint.",
                    MessageType.None);
            }
        }

        private void DrawApplyPanel()
        {
            EditorGUILayout.LabelField("Output", EditorStyles.boldLabel);

            if (GUILayout.Button("Apply To CameraBounds_Outer", GUILayout.Height(30f)))
                ApplySelectionToOutput();
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            var evt = Event.current;
            if (evt == null)
                return;

            UpdateHoverPath(evt.mousePosition);
            DrawSceneContours();

            if (!m_paintMode)
                return;

            if (evt.type == EventType.KeyDown && evt.keyCode == KeyCode.Escape)
            {
                m_paintMode = false;
                Repaint();
                evt.Use();
                return;
            }

            if (!TryGetMouseWorldPosition(evt.mousePosition, out var worldPosition))
                return;

            m_hoverWorldPosition = worldPosition;
            m_hoverPathIndex = FindPathAtWorldPoint(worldPosition);

            Handles.color = new Color(1f, 0.9f, 0.2f, 0.9f);
            Handles.DrawWireDisc(worldPosition, Vector3.forward, 0.25f);

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (evt.type == EventType.MouseDown && evt.button == 0 && !evt.alt)
            {
                PaintSelectionAtHover();
                evt.Use();
            }

            sceneView.Repaint();
        }

        private void DrawSceneContours()
        {
            if (m_source == null || m_paths.Count == 0)
                return;

            for (var i = 0; i < m_paths.Count; i++)
            {
                var isSelected = m_selectedPathIndices.Contains(i);
                var isHover = i == m_hoverPathIndex;

                var color = new Color(1f, 1f, 1f, 0.25f);
                if (isSelected)
                    color = new Color(0.2f, 1f, 0.45f, 0.9f);
                if (isHover)
                    color = new Color(1f, 0.85f, 0.2f, 1f);

                DrawPathWire(m_paths[i].Points, color, m_lineWidth);

                if (m_showLabels)
                {
                    var worldCenter = m_source.transform.TransformPoint(m_paths[i].Centroid);
                    Handles.Label(worldCenter, $"{i + 1}");
                }
            }
        }

        private void PaintSelectionAtHover()
        {
            if (m_hoverPathIndex < 0 || m_hoverPathIndex >= m_paths.Count)
                return;

            switch (m_brushMode)
            {
                case BrushMode.Add:
                    m_selectedPathIndices.Add(m_hoverPathIndex);
                    break;
                case BrushMode.Remove:
                    m_selectedPathIndices.Remove(m_hoverPathIndex);
                    break;
                case BrushMode.Toggle:
                    if (!m_selectedPathIndices.Add(m_hoverPathIndex))
                        m_selectedPathIndices.Remove(m_hoverPathIndex);
                    break;
            }

            Repaint();
            SceneView.RepaintAll();
        }

        private void ApplySelectionToOutput()
        {
            if (m_source == null)
            {
                EditorUtility.DisplayDialog("Camera Bounds", "Bounds Composite is not set.", "OK");
                return;
            }

            if (m_selectedPathIndices.Count == 0)
            {
                EditorUtility.DisplayDialog("Camera Bounds", "No paths selected.", "OK");
                return;
            }

            var selectedLocalPaths = BuildSelectedPathList();
            if (selectedLocalPaths.Count == 0)
            {
                EditorUtility.DisplayDialog("Camera Bounds", "No valid selected paths found.", "OK");
                return;
            }

            if (CameraBoundsGenerator.ApplyToOutput(m_source, selectedLocalPaths, m_confiner, out var outputObject))
            {
                Debug.Log(
                    $"[CameraBoundsPainter] Applied paths: {selectedLocalPaths.Count}, source: {m_source.name}.",
                    outputObject);
            }
        }

        private List<Vector2[]> BuildSelectedPathList()
        {
            var selected = new List<int>(m_selectedPathIndices);
            selected.Sort((a, b) => m_paths[b].AbsArea.CompareTo(m_paths[a].AbsArea));

            var result = new List<Vector2[]>(selected.Count);
            for (var i = 0; i < selected.Count; i++)
            {
                var index = selected[i];
                if (index < 0 || index >= m_paths.Count)
                    continue;

                result.Add(m_paths[index].Points);
            }

            return result;
        }

        private void RebuildPaths(bool resetSelectionToLargest)
        {
            m_paths.Clear();
            if (m_source != null)
                m_paths.AddRange(CameraBoundsGenerator.ExtractAllPaths(m_source));

            if (resetSelectionToLargest)
            {
                SelectLargestOnly();
                return;
            }

            var stale = new List<int>();
            foreach (var index in m_selectedPathIndices)
            {
                if (index < 0 || index >= m_paths.Count)
                    stale.Add(index);
            }

            for (var i = 0; i < stale.Count; i++)
                m_selectedPathIndices.Remove(stale[i]);
        }

        private void SelectLargestOnly()
        {
            m_selectedPathIndices.Clear();
            if (m_paths.Count == 0)
                return;

            var largestIndex = 0;
            var largestArea = m_paths[0].AbsArea;
            for (var i = 1; i < m_paths.Count; i++)
            {
                if (m_paths[i].AbsArea <= largestArea)
                    continue;

                largestArea = m_paths[i].AbsArea;
                largestIndex = i;
            }

            m_selectedPathIndices.Add(largestIndex);
        }

        private void SelectAll()
        {
            m_selectedPathIndices.Clear();
            for (var i = 0; i < m_paths.Count; i++)
                m_selectedPathIndices.Add(i);
        }

        private int FindPathAtWorldPoint(Vector3 worldPoint)
        {
            if (m_source == null || m_paths.Count == 0)
                return -1;

            var local = (Vector2)m_source.transform.InverseTransformPoint(worldPoint);

            var bestIndex = -1;
            var bestArea = float.MaxValue;
            for (var i = 0; i < m_paths.Count; i++)
            {
                if (!IsPointInPolygon(local, m_paths[i].Points))
                    continue;

                if (m_paths[i].AbsArea >= bestArea)
                    continue;

                bestArea = m_paths[i].AbsArea;
                bestIndex = i;
            }

            return bestIndex;
        }

        private void UpdateHoverPath(Vector2 guiMousePosition)
        {
            if (!TryGetMouseWorldPosition(guiMousePosition, out var worldPosition))
                return;

            m_hoverWorldPosition = worldPosition;
            m_hoverPathIndex = FindPathAtWorldPoint(worldPosition);
        }

        private void AutoResolveReferences()
        {
            if (m_source == null)
                m_source = CameraBoundsGenerator.ResolveSourceComposite();

            if (m_confiner == null)
                m_confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
        }

        private void DrawPathWire(Vector2[] localPoints, Color color, float lineWidth)
        {
            if (m_source == null || localPoints == null || localPoints.Length < 2)
                return;

            var worldPoints = new Vector3[localPoints.Length + 1];
            for (var i = 0; i < localPoints.Length; i++)
                worldPoints[i] = m_source.transform.TransformPoint(localPoints[i]);

            worldPoints[localPoints.Length] = worldPoints[0];

            Handles.color = color;
            Handles.DrawAAPolyLine(lineWidth, worldPoints);
        }

        private static bool IsPointInPolygon(Vector2 point, IReadOnlyList<Vector2> polygon)
        {
            var inside = false;
            if (polygon == null || polygon.Count < 3)
                return false;

            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                var pi = polygon[i];
                var pj = polygon[j];

                var intersects = ((pi.y > point.y) != (pj.y > point.y)) &&
                                 (point.x < (pj.x - pi.x) * (point.y - pi.y) / ((pj.y - pi.y) + Mathf.Epsilon) + pi.x);

                if (intersects)
                    inside = !inside;
            }

            return inside;
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
    }
}

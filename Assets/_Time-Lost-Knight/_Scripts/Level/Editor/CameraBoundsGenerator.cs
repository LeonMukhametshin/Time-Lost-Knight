using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class CameraBoundsGenerator
{
    private const string GenerateMenuPath = "Tools/Level/Camera Bounds/Generate From Bounds (Outer)";
    private const string OpenPainterMenuPath = "Tools/Level/Camera Bounds/Open Painter";

    internal const string DefaultSourceName = "Bounds";
    internal const string DefaultOutputName = "CameraBounds_Outer";

    [MenuItem(GenerateMenuPath)]
    private static void GenerateBase()
    {
        var source = ResolveSourceComposite();
        if (source == null)
        {
            EditorUtility.DisplayDialog(
                "Camera Bounds",
                "CompositeCollider2D not found. Select Bounds (or any object with CompositeCollider2D) and try again.",
                "OK");
            return;
        }

        if (source.pathCount <= 0)
        {
            EditorUtility.DisplayDialog(
                "Camera Bounds",
                "Selected CompositeCollider2D has no paths.",
                "OK");
            return;
        }

        var largestPath = ExtractLargestPath(source);
        if (largestPath == null || largestPath.Length < 3)
        {
            EditorUtility.DisplayDialog(
                "Camera Bounds",
                "Failed to extract base outer contour from Bounds.",
                "OK");
            return;
        }

        var basePaths = new List<Vector2[]>(1) { largestPath };
        var confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
        if (!ApplyToOutput(source, basePaths, confiner, out var outputObject))
            return;

        Debug.Log(
            $"[CameraBoundsGenerator] Base generated. Source: {source.name}, paths: 1, points: {largestPath.Length}, assignedConfiner: {confiner != null}.",
            outputObject);
    }

    [MenuItem(OpenPainterMenuPath)]
    private static void OpenPainterWindow()
    {
        CameraBoundsPainterWindow.OpenWindow();
    }

    internal static CompositeCollider2D ResolveSourceComposite()
    {
        if (Selection.activeTransform != null)
        {
            var selected = Selection.activeTransform.GetComponent<CompositeCollider2D>();
            if (selected != null)
                return selected;
        }

        var byName = GameObject.Find(DefaultSourceName);
        if (byName != null && byName.TryGetComponent(out CompositeCollider2D byNameComposite))
            return byNameComposite;

        return Object.FindAnyObjectByType<CompositeCollider2D>();
    }

    internal static List<PathData> ExtractAllPaths(CompositeCollider2D source)
    {
        const float areaEpsilon = 0.0001f;
        var result = new List<PathData>();
        if (source == null)
            return result;

        for (var i = 0; i < source.pathCount; i++)
        {
            var pointCount = source.GetPathPointCount(i);
            if (pointCount < 3)
                continue;

            var points = new Vector2[pointCount];
            source.GetPath(i, points);

            var signedArea = GetSignedArea(points);
            var absArea = Mathf.Abs(signedArea);
            if (absArea < areaEpsilon)
                continue;

            var centroid = CalculateCentroid(points);
            result.Add(new PathData(i, points, signedArea, absArea, centroid));
        }

        return result;
    }

    internal static Vector2[] ExtractLargestPath(CompositeCollider2D source)
    {
        var paths = ExtractAllPaths(source);
        if (paths.Count == 0)
            return null;

        var maxArea = float.MinValue;
        var largestPath = paths[0].Points;

        for (var i = 0; i < paths.Count; i++)
        {
            if (paths[i].AbsArea <= maxArea)
                continue;

            maxArea = paths[i].AbsArea;
            largestPath = paths[i].Points;
        }

        return largestPath;
    }

    internal static bool ApplyToOutput(
        CompositeCollider2D source,
        IReadOnlyList<Vector2[]> paths,
        CinemachineConfiner2D confiner,
        out GameObject outputObject)
    {
        outputObject = null;

        if (source == null || paths == null || paths.Count == 0)
            return false;

        Undo.IncrementCurrentGroup();
        Undo.SetCurrentGroupName("Generate Camera Bounds");
        var undoGroup = Undo.GetCurrentGroup();

        outputObject = GetOrCreateOutputObject(source.transform);
        SyncTransform(outputObject.transform, source.transform);

        var polygon = outputObject.GetComponent<PolygonCollider2D>();
        if (polygon == null)
            polygon = Undo.AddComponent<PolygonCollider2D>(outputObject);

        Undo.RecordObject(polygon, "Update camera bounds collider");
        polygon.pathCount = paths.Count;
        for (var i = 0; i < paths.Count; i++)
        {
            polygon.SetPath(i, paths[i]);
        }
        polygon.isTrigger = true;

        if (confiner == null)
            confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();

        if (confiner != null)
        {
            Undo.RecordObject(confiner, "Assign camera confiner bounds");
            confiner.BoundingShape2D = polygon;
            confiner.InvalidateCache();
        }

        var scene = outputObject.scene;
        if (scene.IsValid())
            EditorSceneManager.MarkSceneDirty(scene);

        Undo.CollapseUndoOperations(undoGroup);
        Selection.activeGameObject = outputObject;
        return true;
    }

    internal static float GetSignedArea(Vector2[] points)
    {
        if (points == null || points.Length < 3)
            return 0f;

        var area = 0f;
        for (var i = 0; i < points.Length; i++)
        {
            var a = points[i];
            var b = points[(i + 1) % points.Length];
            area += (a.x * b.y) - (b.x * a.y);
        }

        return area * 0.5f;
    }

    private static Vector2 CalculateCentroid(IReadOnlyList<Vector2> points)
    {
        var center = Vector2.zero;
        if (points == null || points.Count == 0)
            return center;

        for (var i = 0; i < points.Count; i++)
            center += points[i];

        return center / points.Count;
    }

    private static GameObject GetOrCreateOutputObject(Transform sourceTransform)
    {
        var parent = sourceTransform.parent;
        var existing = parent != null
            ? parent.Find(DefaultOutputName)
            : GameObject.Find(DefaultOutputName)?.transform;

        if (existing != null)
            return existing.gameObject;

        var created = new GameObject(DefaultOutputName);
        Undo.RegisterCreatedObjectUndo(created, "Create camera bounds");

        if (parent != null)
            created.transform.SetParent(parent, false);

        return created;
    }

    private static void SyncTransform(Transform target, Transform source)
    {
        Undo.RecordObject(target, "Sync camera bounds transform");
        target.SetParent(source.parent, false);
        target.localPosition = source.localPosition;
        target.localRotation = source.localRotation;
        target.localScale = source.localScale;
    }

    internal readonly struct PathData
    {
        public PathData(int sourcePathIndex, Vector2[] points, float signedArea, float absArea, Vector2 centroid)
        {
            SourcePathIndex = sourcePathIndex;
            Points = points;
            SignedArea = signedArea;
            AbsArea = absArea;
            Centroid = centroid;
        }

        public int SourcePathIndex { get; }
        public Vector2[] Points { get; }
        public float SignedArea { get; }
        public float AbsArea { get; }
        public Vector2 Centroid { get; }
    }
}

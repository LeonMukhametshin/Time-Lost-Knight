using System;
using UnityEngine;

[Serializable]
public class DashVizualizer
{
    [SerializeField] private Transform indecator;

    private const float deflectionAngle = 45.0f;

    public void SetActive(bool active) =>
        indecator.gameObject.SetActive(active);

    public void SetRotation(float angle) =>
        indecator.rotation = Quaternion.Euler(0f, 0f, angle - deflectionAngle);
}
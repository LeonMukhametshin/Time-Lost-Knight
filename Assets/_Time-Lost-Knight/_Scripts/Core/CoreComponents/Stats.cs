using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth = 100f;

    [field: SerializeField] public HealthSystem healthSystem { get; private set; }

    public override void Awake()
    {
        base.Awake();

        healthSystem.Initialize(maxHealth);
    }

    public void DecreaseHealth(float value) =>
         healthSystem.Decrease(value);

    public void IncreaseHealth(float value) =>
          healthSystem.Increase(value);
}
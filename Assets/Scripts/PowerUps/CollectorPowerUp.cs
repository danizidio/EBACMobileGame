using UnityEngine;

public class CollectorPowerUp : PowerUpBase
{
    [Header("Mega Collector")]
    [SerializeField] Vector3 _colliderSize;

    protected override void StartPowerUp()
    {
        p.CollectorBuff(_colliderSize);

        base.StartPowerUp();
    }

    protected override void EndPowerUp()
    {
        p.CollectorBuff(Vector3.one);

        base.EndPowerUp();
    }
}

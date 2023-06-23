using UnityEngine;

public class FlyPowerUp : PowerUpBase
{
    [Header("Fly Power Up")]
    [SerializeField] float _heightAmount;

    protected override void StartPowerUp()
    {
        p.HeightBuff(_heightAmount);

        base.StartPowerUp();
    }

    protected override void EndPowerUp()
    {
        p.HeightBuff(0);

        base.EndPowerUp();
    }
}

using UnityEngine;

public class SpeedPowerUp : PowerUpBase
{
    [Header("Speed Power Up")]

    [SerializeField] float _speedAmount;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();

        p.SpeedBuff(_speedAmount);
    }

    protected override void EndPowerUp()
    {
        p.SpeedBuff(1);

        base.EndPowerUp();
    }
}

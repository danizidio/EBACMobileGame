
public class InvinciblePowerUp : PowerUpBase
{
    protected override void StartPowerUp()
    {
        p?.InvincibleBuff(true);

        base.StartPowerUp();
    }

    protected override void EndPowerUp()
    {
        p?.InvincibleBuff(false);

        base.EndPowerUp();
    }
}

using UnityEngine;

public class PowerUpBase : CollectibleItens
{
    [SerializeField] SO_PowerUpTimer sO_Time;
    float _maxTime;

    private void Start()
    {
        _maxTime = sO_Time.MaxTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        p = other.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        CollectedItem();
    }

    protected override void CollectedItem()
    {
        base.CollectedItem();

        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Invoke("EndPowerUp", _maxTime);
    }

    protected virtual void EndPowerUp()
    {
        Destroy(gameObject);
    }

}

using UnityEngine;

public class CollectibleItens : MonoBehaviour
{
    [SerializeField] GameObject _coinTakeFX;

    protected PlayerBehaviour p;

    protected virtual void CollectedItem()
    {
        Instantiate(_coinTakeFX, this.transform.position, Quaternion.identity);
    }
}

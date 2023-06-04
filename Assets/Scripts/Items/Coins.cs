using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] GameObject _coinTakeFX;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour p = other.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        Instantiate(_coinTakeFX, this.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

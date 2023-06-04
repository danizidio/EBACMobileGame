using Unity.VisualScripting;
using UnityEngine;

public class LerpFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float _lerpSpeed;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, _lerpSpeed * Time.deltaTime); 
    }
}

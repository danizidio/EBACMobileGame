using DG.Tweening;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 50, RotateMode.FastBeyond360).SetLoops(100);
    }
}

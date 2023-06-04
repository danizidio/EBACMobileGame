
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    Vector3 _positionToGo;

    [SerializeField] float _forwardSpeed;
    [SerializeField] float _velocity;

    private void Update()
    {
        transform.position += Vector3.right * _forwardSpeed * Time.deltaTime;

        if (Input.GetMouseButton(0))
            Moving(Input.mousePosition.x - _positionToGo.x);

        _positionToGo = Input.mousePosition;
    }

    void Moving(float m)
    {
        transform.position -= Vector3.forward * Time.deltaTime * m * _velocity;
    }
}

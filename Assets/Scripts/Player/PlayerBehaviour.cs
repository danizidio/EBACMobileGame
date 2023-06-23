using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action OnActing;
    public event Action OnPausing;

    Rigidbody _rb;

    [SerializeField] Transform _footDetector;
    [SerializeField] GameObject _coinCollector;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    SaveLoad s;

    Animator _anim;

    [SerializeField] float _forwardSpeed;
    [SerializeField] float _speedbonus = 1;

    float _heightBonus = 0;

    [SerializeField] float _horizontalVelocity;

    Vector3 _positionToGo;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _canMove = true;
    }

    void LateUpdate()
    {
        transform.position += Vector3.right * _forwardSpeed * _speedbonus * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + _heightBonus,0);

        if (Input.GetMouseButton(0))
            Moving(Input.mousePosition.x - _positionToGo.x);

        _positionToGo = Input.mousePosition;
    }

    void Moving(float m)
    {
        //transform.position -= Vector3.forward * Time.deltaTime * m * _horizontalVelocity;
        _rb.velocity -= Vector3.forward * Time.deltaTime * m * _horizontalVelocity;
    }

    #region - InputManager Buttons
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_canMove)
        {
          
        }
    }

    public void OnTouchMove(InputAction.CallbackContext context)
    {
        if (_canMove && context.performed)
           


        if (context.canceled)
            _positionToGo.z = 0;
    }

    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnActing?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPausing?.Invoke();
        }
    }

    #endregion

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public void SpeedBuff(float speed)
    {
        _speedbonus = speed;
    }

    public void HeightBuff(float height)
    {
        _heightBonus = height;
    }

    public void InvincibleBuff(bool active)
    {
        if(active)
        {
            gameObject.layer = 12;
        }
        else
        {
            gameObject.layer = 3;
        }
    }

    public void CollectorBuff(Vector3 size)
    {
        _coinCollector.transform.localScale = size;
    }
}
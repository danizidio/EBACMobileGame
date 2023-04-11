using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action OnActing;
    public event Action OnPausing;

    Rigidbody rb;

    [SerializeField] float _walkSpeed;
    float _runSpeed;
    float _move;
    float _moveX;

    bool _isRunning;

    [SerializeField] Transform _footDetector;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    SaveLoad s;

    Animator _anim;

    private void Start()
    {
        _runSpeed = _walkSpeed * 2;

        rb = GetComponent<Rigidbody>();

        _canMove = true;
    }

    void LateUpdate()
    {
        if (OnActing == null)
        {
            OnActing = Attacking;
        }

        if (!_isRunning)
        {
            _isRunning = false;
           _move = _walkSpeed;
        }

        if (_moveX == 0)
        {
           // _anim.SetBool("WALK", false);
        }
        else
        {
            //_anim.SetBool("WALK", true);
        }

        if (_canMove == false)
        {
            _moveX = 0;
        }

        rb.velocity = new Vector3(0, 0, _moveX * _runSpeed);
    }

    #region - InputManager Buttons
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_canMove)
        {
            _moveX = context.ReadValue<Vector2>().x;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        if (_isRunning)
        {
            _move = _runSpeed;
        }
        else
        {
            _runSpeed = _walkSpeed;
        }
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
    public void Attacking()
    {
        _anim.SetTrigger("ATTACK");
    }

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }
}
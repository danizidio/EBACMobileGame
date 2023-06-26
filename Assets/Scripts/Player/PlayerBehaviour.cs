using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;
using System.Collections;

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

    [SerializeField] Animator _anim;

    [SerializeField] float _forwardSpeed;
    [SerializeField] float _speedbonus = 1;

    float _heightBonus = 0;

    [SerializeField] float _horizontalVelocity;

    Vector3 _positionToGo;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _canMove = true;
    }

    void LateUpdate()
    {
        if (_canMove)
        {
            transform.position += Vector3.right * _forwardSpeed * _speedbonus * Time.deltaTime;
            transform.position += Vector3.up *  _heightBonus;

            if (Input.GetMouseButton(0))
                Moving(Input.mousePosition.x - _positionToGo.x);

            _positionToGo = Input.mousePosition;
        }
        else
        {
            _positionToGo = Vector3.zero;
        }
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

    #region - PlayerBuffs -
    public void SpeedBuff(float speed)
    {
        _speedbonus = speed;
        _anim.speed = speed /2;

        if(speed == 0)
        {
            _anim.speed = 1;
        }
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
    #endregion
    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public void AnimStart()
    {
        _rb.velocity = Vector3.zero;

        _anim.SetTrigger("START");
    }

    public IEnumerator  AnimGameOver()
    {
        _canMove = false;

        _anim.SetTrigger("DEAD");

        yield return new WaitForSeconds(2);
        GameManager.OnNextGameState?.Invoke(GamePlayStates.GAMEOVER);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            _anim.SetBool("JUMP", false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            _anim.SetBool("JUMP",true);
        }
    }
}
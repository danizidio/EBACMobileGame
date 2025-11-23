using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    public delegate void _onSearchingPlayer();
    public static _onSearchingPlayer OnSearchingPlayer;

    public delegate void _onGetFocus(GameObject item);
    public static _onGetFocus OnGetFocus;

    CinemachineCamera _camera;
    [SerializeField] float _cameraSizeMinimum;
    [SerializeField] float _cameraSizeMaximum;
    [SerializeField] float _maxTimeOnFocus;

    GameObject _p;

    void FindPlayer()
    {
        if ((_camera == null))
        {
            _camera = FindAnyObjectByType<CinemachineCamera>();
        }
        if (_camera != null)
        {
            StartCoroutine(CorroutineFindPlayer());
        }
    }

    IEnumerator CorroutineFindPlayer()
    {
        _p = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(.02f);

        if (_p != null)
        {
            _camera.Follow = _p.transform;

            GameManager.instance.PlayerCharacter(_p.GetComponent<PlayerBehaviour>());

            GameManager.OnNextGameState?.Invoke(GamePlayStates.START);

            StopCoroutine(CorroutineFindPlayer());
        }
        else
        {
            yield return new WaitForSeconds(.02f);
        }
    }

    void ObjectToFocus(GameObject item)
    {
        if (_camera.Follow != _p)
        {
            StopCoroutine("CorroutineObjectToFocus");

            _camera.Follow = null;

            StartCoroutine(CorroutineObjectToFocus(item));
        }
        else
        {
            StartCoroutine(CorroutineObjectToFocus(item));
        }
    }

    IEnumerator CorroutineObjectToFocus(GameObject item)
    {
        _camera.Follow = item.transform;

        _camera.Lens.OrthographicSize = _cameraSizeMinimum;

        yield return new WaitForSeconds(_maxTimeOnFocus);

        _camera.Follow = _p.transform;

        _camera.Lens.OrthographicSize = _cameraSizeMaximum;
    }
    private void OnEnable()
    {
        OnSearchingPlayer += FindPlayer;
        OnGetFocus = ObjectToFocus;
    }
    private void OnDisable()
    {
        OnSearchingPlayer -= FindPlayer;
        OnGetFocus = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraBehaviour : MonoBehaviour
{
    public delegate void _onSearchingPlayer();
    public static _onSearchingPlayer OnSearchingPlayer;

    public delegate void _onGetFocus(GameObject item);
    public static _onGetFocus OnGetFocus;

    [SerializeField] float _cameraSizeMinimum;
    [SerializeField] float _cameraSizeMaximum;
    [SerializeField] float _maxTimeOnFocus;

    GameObject _p;

    void FindPlayer()
    {
        StartCoroutine(CorroutineFindPlayer());
    }

    IEnumerator CorroutineFindPlayer()
    {
        _p = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(.02f);

        if (_p != null)
        {
            GetComponent<CinemachineVirtualCamera>().Follow = _p.transform;

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
        if (GetComponent<CinemachineVirtualCamera>().Follow != _p)
        {
            StopCoroutine("CorroutineObjectToFocus");

            GetComponent<CinemachineVirtualCamera>().Follow = null;

            StartCoroutine(CorroutineObjectToFocus(item));
        }
        else
        {
            StartCoroutine(CorroutineObjectToFocus(item));
        }
    }

    IEnumerator CorroutineObjectToFocus(GameObject item)
    {
        GetComponent<CinemachineVirtualCamera>().Follow = item.transform;

        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = _cameraSizeMinimum;

        yield return new WaitForSeconds(_maxTimeOnFocus);

        GetComponent<CinemachineVirtualCamera>().Follow = _p.transform;

        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = _cameraSizeMaximum;
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

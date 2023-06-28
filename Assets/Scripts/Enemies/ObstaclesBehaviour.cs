using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObstacleType
{
    SIMPLE,
    CHECKPOINT,
    ENDGAME
}

public class ObstaclesBehaviour : MonoBehaviour
{
    [SerializeField] ObstacleType _obstacleType;

    [SerializeField] bool _move;

    [SerializeField] List<Transform> _posList;
    int _index;

    [SerializeField] float _maxTime;

    private void Start()
    {
        if (!_move) return;

        StartCoroutine(Patrolling());
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour p = other.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        if (_obstacleType == ObstacleType.SIMPLE)
           StartCoroutine(p.AnimGameOver());

        if (_obstacleType == ObstacleType.ENDGAME)
        {
            GameManager.instance.CreateNextStage();
        }
    }

    IEnumerator Patrolling()
    {
        float t = 0;
        while (true)
        {
            Vector3 _currentPos = transform.position;

            transform.position = _posList[_index].position ;

            while (t < _maxTime)
            {
                transform.position = Vector3.Lerp(_currentPos, _posList[_index].position, (t / _maxTime));

                t += Time.deltaTime;

                yield return null;
            }
            _index++;

            if (_index >= _posList.Count) _index = 0;

            t = 0;

            yield return null;
        }
    }
}

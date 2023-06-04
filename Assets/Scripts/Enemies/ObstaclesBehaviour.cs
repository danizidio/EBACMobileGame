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

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour p = other.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        if(_obstacleType == ObstacleType.SIMPLE)
        GameManager.OnNextGameState?.Invoke(GamePlayStates.GAMEOVER);

        if(_obstacleType == ObstacleType.ENDGAME)
            GameManager.OnNextGameState?.Invoke(GamePlayStates.FINISH_LINE);
    }
}

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

        if (_obstacleType == ObstacleType.SIMPLE)
           StartCoroutine(p.AnimGameOver());

        if(_obstacleType == ObstacleType.ENDGAME)
            GameManager.OnNextGameState?.Invoke(GamePlayStates.FINISH_LINE);
    }
}

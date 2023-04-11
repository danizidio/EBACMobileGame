using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class GameManager : GamePlayBehaviour
{

    private void Start()
    {
        OnNextGameState(GamePlayStates.INITIALIZING);
    }

    private void Update()
    {
        StateBehaviour(GamePlayCurrentState);

        UpdateState();
    }

    void StateBehaviour(GamePlayStates state)
    {
        switch(state)
        {
            case GamePlayStates.INITIALIZING:
                {
                    CameraBehaviour.OnSearchingPlayer?.Invoke();

                    break;
                }
            case GamePlayStates.START:
                {

                    OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

                    break;
                }
            case GamePlayStates.GAMEPLAY:
                {
                    Time.timeScale = 1;

                    PauseGame();

                    break;
                }
            case GamePlayStates.PAUSE:
                {
                    Time.timeScale = 0;

                    PauseGame();

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    Time.timeScale = 0;

                    break;
                }
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetCurrentGameState() != GamePlayStates.PAUSE)
            {
                OnNextGameState?.Invoke(GamePlayStates.PAUSE);
            }
            else
            {
                OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
            }
        }
    }
}

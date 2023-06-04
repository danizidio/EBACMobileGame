using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : GamePlayBehaviour
{
    [SerializeField] TMP_Text _txt;
    [SerializeField] GameObject _pauseMenu;
    private void Start()
    {
        Time.timeScale = 1;

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
                    _txt.text = "";

                    _pauseMenu.SetActive(false);

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

                    _txt.text = "PAUSE";

                    PauseGame();

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    Time.timeScale = 0;

                    _txt.text = "GAME OVER \n =(";

                    _pauseMenu.SetActive(true);

                    if (Input.GetMouseButton(0))
                    {
                        ChangeScene.OnReloadScene?.Invoke();
                    }

                    break;
                }
            case GamePlayStates.FINISH_LINE:
                {
                    Time.timeScale = 0;

                    _txt.text = "YOU WIN!!";

                    _pauseMenu.SetActive(true);

                    if (Input.GetMouseButton(0))
                    {
                        ChangeScene.OnReloadScene?.Invoke();
                    }

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
                _pauseMenu.SetActive(true);
                OnNextGameState?.Invoke(GamePlayStates.PAUSE);
            }
            else
            {
                _pauseMenu.SetActive(false);
                OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
            }
        }
    }
}
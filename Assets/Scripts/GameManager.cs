using UnityEngine;
using StateMachine;
using TMPro;

public class GameManager : GamePlayBehaviour
{
    public static GameManager instance;

    [SerializeField] TMP_Text _txt;
    [SerializeField] GameObject _pauseMenu;

    PlayerBehaviour _playerBehaviour;

    [SerializeField] int _maxStages;
    int _currentStageIndex;

    [SerializeField] int _stageLentgh;
    [SerializeField] GameObject _startPath;
    [SerializeField] GameObject _endPath;
    [SerializeField] GameObject[] _pathObjs;

    GameObject _lastSpawnedPath;

    [SerializeField] Material[] _material;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 1;

        CreateNextStage();

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
                    _txt.text = "TOUCH TO START";

                    if (Input.GetMouseButton(0))
                    {
                        _txt.text = "";

                        _pauseMenu.SetActive(false);

                        StartGame();
                    }

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

                    _txt.text = "Touch to restart \n \n GAME OVER \n =(";

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

                    _txt.text = "Touch to restart \n \n YOU WIN!!";

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

    public void StartGame()
    {
       OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

        _playerBehaviour.GetComponent<PlayerBehaviour>().enabled = true;
        StartCoroutine(_playerBehaviour.AnimStart());
        _playerBehaviour.ResetMoveValues();
    }

    public void CreateNextStage()
    {
        _currentStageIndex++;

        if (_currentStageIndex <= _maxStages)
        {
            Material m = _material[Random.Range(0, _material.Length)];

            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                if (_lastSpawnedPath == null)
                {
                    _lastSpawnedPath = Instantiate(_startPath);
                    _lastSpawnedPath.transform.position = Vector3.zero;

                    _lastSpawnedPath.GetComponent<PathwayBase>().ChangeScenarioMaterial(m);
                }
                else
                {
                    GameObject temp = Instantiate(_startPath);
                    temp.GetComponent<PathwayBase>().startPath.position = _lastSpawnedPath.GetComponent<PathwayBase>().endPath.position;
                    _lastSpawnedPath = temp;

                    _lastSpawnedPath.GetComponent<PathwayBase>().ChangeScenarioMaterial(m);
                }
            }

            for (int i = 0; i < _stageLentgh; i++)
            {
                GameObject temp = Instantiate(_pathObjs[Random.Range(0, _pathObjs.Length)]);
                temp.GetComponent<PathwayBase>().startPath.position = _lastSpawnedPath.GetComponent<PathwayBase>().endPath.position;
                _lastSpawnedPath = temp;

                _lastSpawnedPath.GetComponent<PathwayBase>().ChangeScenarioMaterial(m);
            }

            for (int i = 0; i < 1; i++)
            {
                GameObject temp = Instantiate(_endPath);
                temp.GetComponent<PathwayBase>().startPath.position = _lastSpawnedPath.GetComponent<PathwayBase>().endPath.position;
                _lastSpawnedPath = temp;

                _lastSpawnedPath.GetComponent<PathwayBase>().ChangeScenarioMaterial(m);
            }
        }
        else
        {
            OnNextGameState(GamePlayStates.FINISH_LINE);
        }
    }

    public PlayerBehaviour PlayerCharacter(PlayerBehaviour playerBehaviour)
    {
        return _playerBehaviour = playerBehaviour;
    }
}

using UnityEngine;
using StateMachine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

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

    [SerializeField] List<PathwayBase> _spawnedPieces = new List<PathwayBase>();

    [SerializeField] GameObject _lastSpawnedPath;

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
        ClearList();

        _currentStageIndex++;

        if (_currentStageIndex <= _maxStages)
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                CreatePiece(_startPath);
            }

            for (int i = 0; i < _stageLentgh; i++)
            {
                CreatePiece(_pathObjs[Random.Range(0, _pathObjs.Length)]);
            }

            for (int i = 0; i < 1; i++)
            {
                CreatePiece(_endPath);
            }

            ChangeMaterialColor();

            StartCoroutine(ScaleEffect());
        }
        else
        {
            OnNextGameState(GamePlayStates.FINISH_LINE);
        }
    }

    void ClearList()
    {
        _spawnedPieces.Clear();
    }

    void CreatePiece(GameObject piece)
    {
        if (_lastSpawnedPath == null)
        {
            _lastSpawnedPath = Instantiate(piece);
            _lastSpawnedPath.transform.position = Vector3.zero;       
        }
        else
        {
            GameObject temp = Instantiate(piece);
            temp.GetComponent<PathwayBase>().startPath.position = _lastSpawnedPath.GetComponent<PathwayBase>().endPath.position;
           
            _lastSpawnedPath = temp;
        }

        _spawnedPieces.Add(_lastSpawnedPath.GetComponent<PathwayBase>());
    }

    void ChangeMaterialColor()
    {
        Material m = _material[Random.Range(0, _material.Length)];

        foreach (var piece in _spawnedPieces)
        {
            piece.GetComponent<PathwayBase>().ChangeScenarioMaterial(m);
        }
    }

    IEnumerator ScaleEffect()
    {
        foreach (var piece in _spawnedPieces)
        {
            piece.transform.localScale = Vector3.zero;
        }
       
        yield return null;

        for (int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, .2f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(.1f);

            _spawnedPieces[i].GetComponent<PathwayBase>().ChangeCoinPos();
            _spawnedPieces[i].GetComponent<PathwayBase>().ChangeObstaclesPos();
        }
    }

    public PlayerBehaviour PlayerCharacter(PlayerBehaviour playerBehaviour)
    {
        return _playerBehaviour = playerBehaviour;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathwayBase : MonoBehaviour
{
    [SerializeField] Transform _endPath;
    public Transform endPath { get { return _endPath; } }

    [SerializeField] Transform _startPath;
    public Transform startPath { get { return _startPath; } }

    [SerializeField] GameObject[] _ground;

    [SerializeField] bool _hasCoins;
    [SerializeField] bool _randomObstacles;
    [SerializeField] float[] _coinPos;
    [SerializeField] float[] _objPos;

    private void Start()
    {    
        ChangeCoinPos();
        ChangeObstaclesPos();
    }

    void ChangeCoinPos()
    {
        if (!_hasCoins) return;

        Coins[] coins = GetComponentsInChildren<Coins>();

        foreach (Coins coin in coins)
        {
            coin.GetComponent<Transform>().position =
            new Vector3(coin.transform.position.x,
            coin.transform.position.y,
            coin.transform.position.z + _coinPos[Random.Range(0, _coinPos.Length)]);
        }
    }

    void ChangeObstaclesPos()
    {
        if (!_randomObstacles) return;

        ObstaclesBehaviour[] obstacles = GetComponentsInChildren<ObstaclesBehaviour>();

        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<Transform>().position =
                new Vector3(obstacle.transform.position.x,
                obstacle.transform.position.y,
                obstacle.transform.position.z + _objPos[Random.Range(0, _objPos.Length)]);
        }

    }

    public void ChangeScenarioMaterial(Material material)
    {
        foreach (var ground in _ground)
        {
            ground.GetComponent<MeshRenderer>().material = material;
        }
    }
}

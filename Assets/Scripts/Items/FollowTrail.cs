using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Update()
    {    
        transform.position = Vector3.MoveTowards(this.transform.position,
            GameManager.instance.LastSpawnedPos().position, _speed);
    }
}

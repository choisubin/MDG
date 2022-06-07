using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField] private Transform[] _foodPos;
    [SerializeField] private float speed = 5f;
    private int foodNum = 0;

    private void Start()
    {
        foodNum = 0;
        transform.position = _foodPos[foodNum].transform.position;
    }

    private void Update()
    {
        MovePath();
    }

    private void MovePath()
    {
        if (foodNum == _foodPos.Length)
            return;

        transform.position =
            Vector2.MoveTowards(transform.position, _foodPos[foodNum].transform.position, speed * Time.deltaTime);

        if (transform.position == _foodPos[foodNum].transform.position)
            foodNum++;

    }
}

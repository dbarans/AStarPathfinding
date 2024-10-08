using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float speed = 2f;
    private float timeElapsed = 0f;




    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnInterval)
        {
            Spawn();
            timeElapsed = 0f;
        }
    }
    private void Spawn()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<BallMovement>().speed = speed;    
    }
    
}

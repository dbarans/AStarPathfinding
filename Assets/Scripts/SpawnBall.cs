using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float spawnInterval = 1f;
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
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [Range(1f, 10f)]
    [SerializeField] float spawnInterval = 3f;
    private float _currentSpawnTime = 0f;


    private void Start()
    {
        _currentSpawnTime = spawnInterval;
    }

    private void Update()
    {
        _currentSpawnTime -= Time.deltaTime;

        if(_currentSpawnTime <= 0f)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            _currentSpawnTime = spawnInterval;
        }
    }




    private void OnDrawGizmos()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoints[i].position, 1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnemySpawnPoint : Enemy
{
    public List<GameObject> enemyPool;
    public float spawnInterval;
    public float spawnIntervalTime;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = 2;
        enemyCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        spawnIntervalTime += Time.deltaTime;

        if (spawnIntervalTime >= spawnInterval && enemyCount > 0)
        {
            SpawnEnemy();
        }

    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPool.Count);

        LeanPool.Spawn(enemyPool[index], transform.position, Quaternion.identity);
        spawnIntervalTime = 0;
        spawnInterval = 2 + GameManager.subspaceDisruptionSystem.subspaceDisruptionTargetValue / 5f;
        enemyCount--;
        
    }
}

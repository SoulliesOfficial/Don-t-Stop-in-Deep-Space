using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnemySpawnPoint : Enemy
{
    public List<string> enemyPool;
    public float spawnInterval;
    public float spawnIntervalTime;
    public int enemyCount;
    public bool spawnOn;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = 2;
        enemyCount = 10;
        spawnOn = false;
        player = GameManager.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.nowRoom == this.room && spawnOn == false)
        {
            spawnOn = true;
            
        }
        else if(player.nowRoom != this.room && spawnOn == true)
        {
            spawnOn = false;
        }


        if (spawnOn)
        {
            spawnIntervalTime += Time.deltaTime;

            if (spawnIntervalTime >= spawnInterval && enemyCount > 0)
            {
                SpawnEnemy();
            }
        }
        

    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPool.Count);

        Enemy e = GenerateEnemy(enemyPool[index], this.room, transform.position - new Vector3(this.room.roomCenter.x, this.room.roomCenter.y, 0));

        spawnIntervalTime = 0;
        spawnInterval = 2 + GameManager.subspaceDisruptionSystem.subspaceDisruptionTargetValue / 5f;
        enemyCount--;
        this.room.enemies.Add(e);
        if(enemyCount == 0)
        {
            Destroy(gameObject);
        }
    }
}

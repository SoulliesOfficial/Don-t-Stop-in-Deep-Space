using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ImperialDestroyer : Enemy
{
    public Player player;
    public string state;
    public float rotationSpeed;

    public const float coolDownInterval = 2f;
    public float coolDown = 0f;
    public GameObject enemyBBullet;

    //Idle
    public float idleWanderRange;
    public float idleWanderTimeInterval;
    public float idleWanderTime;
    public Vector2 wanderPosition;
    public float idleSpeed;
    public float pursueSpeed;

    void Start()
    {
        player = GameManager.player;
        state = "Idle";
        health = 4f;
        idleWanderRange = 10f;
        idleWanderTimeInterval = 10f;
        idleWanderTime = 10f;
        idleSpeed = 0.1f;
        rotationSpeed = 1f;
        pursueSpeed = 6f;
    }

    void FixedUpdate()
    {
        if (state == "Idle")
        {
            idleWanderTime += Time.fixedDeltaTime;
            if (idleWanderTime > idleWanderTimeInterval)
            {
                wanderPosition = new Vector2(
                    Random.Range(spawnPosition.x - idleWanderRange, spawnPosition.x + idleWanderRange),
                    Random.Range(spawnPosition.y - idleWanderRange, spawnPosition.y + idleWanderRange));
                idleWanderTime = 0f;
            }

            transform.up = Vector2.Lerp(transform.up, wanderPosition - new Vector2(transform.position.x, transform.position.y), rotationSpeed * Time.fixedDeltaTime);
            GetComponent<Rigidbody2D>().position = Vector2.Lerp(transform.position, wanderPosition, idleSpeed * Time.fixedDeltaTime);

            if ((transform.position - player.transform.position).magnitude <= 30f)
            {
                state = "Pursue";
            }
        }
        else if (state == "Pursue")
        {
            coolDown += Time.deltaTime;
            if (coolDown >= coolDownInterval)
            {
                LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 0), 10f);
                LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 45))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 45), 10f);
                LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -45))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, -45), 10f);
                coolDown = 0;
            }

            transform.up = Vector2.Lerp(transform.up,
                new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y),
                rotationSpeed * Time.fixedDeltaTime);

            float dis = Vector3.Distance(transform.position, player.transform.position);
            float pross = pursueSpeed * Time.fixedDeltaTime / dis;

            GetComponent<Rigidbody2D>().position = Vector2.Lerp(transform.position, player.transform.position, pross);

        }
    }

    public static void GenerateEnemy(SpaceRoom spaceRoom, Vector2 position)
    {
        LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.imperialCorvette, spaceRoom.roomCenter + position, Quaternion.identity);
    }


    public Vector2 RotateVector(Vector2 origin, float angle)
    {
        return new Vector2
            (origin.x * Mathf.Cos(angle * Mathf.Deg2Rad) + origin.y * Mathf.Sin(angle * Mathf.Deg2Rad),
            -origin.x * Mathf.Sin(angle * Mathf.Deg2Rad) + origin.y * Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}

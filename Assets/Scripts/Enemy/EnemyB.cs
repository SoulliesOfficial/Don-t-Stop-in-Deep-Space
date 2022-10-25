using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnemyB : Enemy
{
    public Player player;
    public int state;

    public const float coolDownInterval = 1f;
    public float coolDown = 0f;
    public GameObject enemyBBullet;

    void Start()
    {
        player = GameManager.player;
    }


    private void Update()
    {
        coolDown += Time.deltaTime;

        if (coolDown >= coolDownInterval)
        {
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 0), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 45))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 45), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 90), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 135))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 135), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 180))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 180), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 225))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 225), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 270))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 270), 10f);
            LeanPool.Spawn(enemyBBullet, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 315))).GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, 315), 10f);

            coolDown = 0;
        }
    }


    void FixedUpdate()
    {
        LookAt(transform.position, player.transform.position);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 7);
    }

    private void LookAt(Vector2 oriPos, Vector2 targetPos)
    {
        Vector2 v = targetPos - oriPos;
        transform.up = v;
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

using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UniRx;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject[] enemies;
    public Enemy targetEnemy;
    public string state;
    public float rotationSpeed;
    public Vector2 direction;

    public float speed;

    void Start()
    {
        state = "Forward";
        targetEnemy = null;
        rotationSpeed = 2f;
        //speed = 40f;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void Initialize(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        gameObject.GetComponent<Rigidbody2D>().velocity = (direction * GameManager.playerInputManager.player.instantSpeed) + (GameManager.playerInputManager.player.displacement.normalized * GameManager.playerInputManager.player.instantSpeed);
        Observable.Timer(System.TimeSpan.FromSeconds(10)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    void FixedUpdate()
    {
        if(state == "Forward" && targetEnemy == null)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed);

            for (int i = 0; i < enemies.Length; i++)
            {
                float dis = Vector2.Distance(transform.position, enemies[i].transform.position);
                if (dis <= 30)
                {
                    SetTarget(enemies[i].GetComponent<Enemy>());
                }
            }
        }
        else if (state == "Pursue" && targetEnemy != null)
        {
            float frameSpeed = Time.fixedDeltaTime * speed * 2f;

            transform.up = Vector2.Lerp(transform.up,
                new Vector2(targetEnemy.transform.position.x - transform.position.x, targetEnemy.transform.position.y - transform.position.y),
                rotationSpeed * Time.fixedDeltaTime);
            float dis = Vector2.Distance(transform.position, targetEnemy.transform.position);
            float pross = 0;

            if (frameSpeed > dis)
            {
                GetComponent<Rigidbody2D>().position = targetEnemy.transform.position;
            }
            else
            {
                pross = frameSpeed / dis;
                GetComponent<Rigidbody2D>().position = Vector2.Lerp(transform.position, targetEnemy.transform.position, pross);
            }
        }
    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
        this.state = "Pursue";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hurt(2);
            LeanPool.Despawn(gameObject);
        }
        else if (collision.gameObject.tag == "Obscatle")
        {
            LeanPool.Despawn(gameObject);
        }
    }
}

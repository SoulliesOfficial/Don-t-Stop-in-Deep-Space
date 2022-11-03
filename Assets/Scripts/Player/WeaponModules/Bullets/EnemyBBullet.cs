using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UniRx;

public class EnemyBBullet : Bullet
{
    public Vector2 direction;
    public float speed;

    public void Initialize(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = 10;
        gameObject.GetComponent<Rigidbody2D>().velocity = (direction * GameManager.playerInputManager.player.instantSpeed) + (GameManager.playerInputManager.player.displacement.normalized * GameManager.playerInputManager.player.instantSpeed);
        Observable.Timer(System.TimeSpan.FromSeconds(5)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().life -= 25;
            collision.GetComponent<Player>().invincibleTime = 3f;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Obscatle")
        {
            LeanPool.Despawn(gameObject);
        }

    }
}

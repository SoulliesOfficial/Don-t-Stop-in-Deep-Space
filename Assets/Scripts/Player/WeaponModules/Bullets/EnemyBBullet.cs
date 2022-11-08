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
        gameObject.GetComponent<Rigidbody2D>().velocity = (direction * speed);
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
            AudioSource sfxPlayer = GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().sfxPlayer;
            sfxPlayer.PlayOneShot(GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().p_hit);

            if (collision.GetComponent<Player>().invincibleTime < 0)
            {
                GameManager.subspaceDisruptionSystem.initialValue -= 2f;
                collision.GetComponent<Player>().invincibleTime = 3f;
            }
            LeanPool.Despawn(gameObject);
        }
        else if (collision.gameObject.tag == "Obscatle")
        {
            LeanPool.Despawn(gameObject);
        }

    }
}

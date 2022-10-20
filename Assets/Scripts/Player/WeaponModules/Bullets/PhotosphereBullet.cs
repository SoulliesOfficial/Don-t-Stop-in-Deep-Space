using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Lean.Pool;

public class PhotosphereBullet : Bullet
{
    public Vector2 direction;
    public float speed;

    public void Initialize(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        Observable.Timer(System.TimeSpan.FromSeconds(5)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}

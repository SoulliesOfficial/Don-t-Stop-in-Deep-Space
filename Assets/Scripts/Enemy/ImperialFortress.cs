using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Lean.Pool;

public class ImperialFortress : Enemy
{
    public int order0, order1;
    public float order0TimeInterval;

    public string state;

    public float rotationSpeed = 2f;
    public float powerRushTimeInterval, powerRushTime;
    public float snipeTimeInterval, snipeTime;
    public float expandFieldInterval, expandFieldTime;
    public float produceShipsInterval, produceShipsTime;

    public bool fortressEnabled;

    public Player player;

    public GameObject bullet;
    public Transform powerRushTransform, snipeTransform, generateEnemyTransform;

    void Start()
    {
        player = GameManager.player;
        health = 100;
        rotationSpeed = 2f;
        powerRushTimeInterval = 20f;
        snipeTimeInterval = 5f;
        expandFieldInterval = 30f;
        produceShipsInterval = 30f;
        fortressEnabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.up = Vector2.Lerp(transform.up,
            new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y),
            rotationSpeed * Time.fixedDeltaTime);
        if(player.nowRoom == this.room && fortressEnabled == false)
        {
            fortressEnabled = true;
            FirePowerRush();
        }

        if (fortressEnabled)
        {
            GameManager.subspaceDisruptionSystem.subspaceDisruptionTargetValue += 100 * Time.fixedDeltaTime;
            snipeTime += Time.fixedDeltaTime;
            if (snipeTime > snipeTimeInterval)
            {
                Snipe();
            }
        }
    }

    void FirePowerRush()
    {
        powerRushTime = 0;
        for (int i = 0; i < 20; i+=2)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(i)).First()
                .Subscribe(_ =>
                {
                    ShootBullet(0 + 15);
                    ShootBullet(30 + 15);
                    ShootBullet(60 + 15);
                    ShootBullet(90 + 15);
                    ShootBullet(-30 + 15);
                    ShootBullet(-60 + 15);
                    ShootBullet(-90 + 15);
                }).AddTo(this);
            Observable.Timer(System.TimeSpan.FromSeconds(i + 0.5f)).First()
                .Subscribe(_ =>
                {
                    ShootBullet(0);
                    ShootBullet(30);
                    ShootBullet(60);
                    ShootBullet(90);
                    ShootBullet(-30);
                    ShootBullet(-60);
                    ShootBullet(-90);
                }).AddTo(this);
            Observable.Timer(System.TimeSpan.FromSeconds(i + 1f)).First()
                .Subscribe(_ =>
                {
                    ShootBullet(0 - 15);
                    ShootBullet(30 - 15);
                    ShootBullet(60 - 15);
                    ShootBullet(90 - 15);
                    ShootBullet(-30 - 15);
                    ShootBullet(-60 - 15);
                    ShootBullet(-90 - 15);
                }).AddTo(this);
            Observable.Timer(System.TimeSpan.FromSeconds(i + 1.5f)).First()
                .Subscribe(_ =>
                {
                    ShootBullet(0);
                    ShootBullet(30);
                    ShootBullet(60);
                    ShootBullet(90);
                    ShootBullet(-30);
                    ShootBullet(-60);
                    ShootBullet(-90);
                }).AddTo(this);
        }
        Observable.Timer(System.TimeSpan.FromSeconds(25)).First().Subscribe(_ => { ProduceShips(); }).AddTo(this);

    }

    void Snipe()
    {
        snipeTimeInterval = Random.Range(2, 10);
        snipeTime = 0;
        ShootBullet(0, 30);
    }

    void ProduceShips()
    {
        produceShipsTime = 0;
        for(int i = 0; i < 5; i++)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(i * 2)).Subscribe(_ =>
            {
                GameObject c = LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.imperialCorvette, generateEnemyTransform.transform.position, Quaternion.identity);
                c.GetComponent<ImperialCorvette>().state = "Pursue";
            });
        }
        Observable.Timer(System.TimeSpan.FromSeconds(25)).First().Subscribe(_ => { FirePowerRush(); }).AddTo(this);

    }

    void ShootBullet(float angle)
    {
       LeanPool.Spawn(bullet, powerRushTransform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, angle)))
            .GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, angle), 10f);
    }

    void ShootBullet(float angle, float speed)
    {
        LeanPool.Spawn(bullet, snipeTransform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, angle)))
             .GetComponent<EnemyBBullet>().Initialize(RotateVector(this.transform.up, angle), speed);
    }



    public Vector2 RotateVector(Vector2 origin, float angle)
    {
        return new Vector2
            (origin.x * Mathf.Cos(angle * Mathf.Deg2Rad) + origin.y * Mathf.Sin(angle * Mathf.Deg2Rad),
            -origin.x * Mathf.Sin(angle * Mathf.Deg2Rad) + origin.y * Mathf.Cos(angle * Mathf.Deg2Rad));
    }

}

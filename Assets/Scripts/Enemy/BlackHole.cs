using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Enemy
{
    public Player player;
    public string state;

    //Idle
    public float idleToAttractTimeInterval;
    public float idleToAttractTime;
    //Attract
    public float attractForce;
    public float attractToIdleTimeInterval;
    public float attractToIdleTime;

    public bool isCatching;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.player;
        state = "Idle";
        health = 10f;
        idleToAttractTimeInterval = 20f;
        idleToAttractTime = 0f;
        attractForce = 50f;
        attractToIdleTimeInterval = 5f;
        attractToIdleTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == "Idle")
        {
            idleToAttractTime += Time.fixedDeltaTime;
            isCatching = false;
            if (idleToAttractTime > idleToAttractTimeInterval)
            {
                idleToAttractTime = 0;
                state = "Attract";
            }
        }
        else if (state == "Attract")
        {
            attractToIdleTime += Time.fixedDeltaTime;
            if (attractToIdleTime > attractToIdleTimeInterval)
            {
                attractToIdleTime = 0;
                state = "Idle";
            }
           
            Vector2 rawDirection = transform.position - player.transform.position;
            Vector2 direction = rawDirection.normalized;
            float distance = rawDirection.magnitude;
            if (distance <= 15f && !isCatching)
            {
                player.GetComponent<Rigidbody2D>().AddForce((15f-distance) * direction * attractForce * Time.fixedDeltaTime);
            }
        }
    }

    public override void Hurt(float damage)
    {
        attractToIdleTime = 999f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(state == "Attract" && collision.gameObject == player.gameObject)
        {
            isCatching = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
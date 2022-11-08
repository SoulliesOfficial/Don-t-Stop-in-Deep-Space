using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ImperialCorvette : Enemy
{

    public Player player;
    public string state;
    public float rotationSpeed;

    //Idle
    public float idleWanderRange;
    public float idleWanderTimeInterval;
    public float idleWanderTime;
    public Vector2 wanderPosition;
    public float idleSpeed;
    //Pursue
    public float pursueSpeed;
    public float pursueToCrushTimeInterval;
    public float pursueTocrushTime;
    //Crush
    public float crushTimeInterval;
    public float crushTime;
    public float crushSpeed;

    void Start()
    {
        player = GameManager.player;
        state = "Idle";
        health = 10f;
        idleWanderRange = 10f;
        idleWanderTimeInterval = 10f;
        idleWanderTime = 10f;
        idleSpeed = 0.1f;
        rotationSpeed = 1f;
        pursueSpeed = 10f;
        crushSpeed = 30f;
        pursueToCrushTimeInterval = 8f;
        pursueTocrushTime = 0f;
        crushTimeInterval = 2f;
        crushTime = 0f;
    }

    // Update is called once per frame
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

            if ((transform.position - player.transform.position).magnitude <= 100f)
            {
                state = "Pursue";
            }
        }
        else if(state == "Pursue")
        {
            pursueTocrushTime += Time.fixedDeltaTime;
            if (pursueTocrushTime >= pursueToCrushTimeInterval)
            {
                this.playedSound = false;
                state = "Crush";
                pursueTocrushTime = 0f;
                crushTime = crushTimeInterval;
                crushSpeed = crushTimeInterval * 30f;
            }

            transform.up = Vector2.Lerp(transform.up,
                new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y),
                rotationSpeed * Time.fixedDeltaTime);

            float dis = Vector3.Distance(transform.position, player.transform.position);
            float pross = pursueSpeed * Time.fixedDeltaTime / dis;
 
            GetComponent<Rigidbody2D>().position = Vector2.Lerp(transform.position, player.transform.position, pross);

        }
        else if(state == "Crush")
        {
            if (crushTime <= 0f)
            {
                state = "Pursue";
            }
            if(this.playedSound == false){
                enemyAudioSource.PlayOneShot(e_dashSound);
                this.playedSound = true;
            }
            

            transform.Translate(Vector2.up * crushSpeed * Time.fixedDeltaTime);
            crushTime -= Time.fixedDeltaTime;
            crushSpeed = crushTime * 30f;
        }


    }

    //private void LookAt(Vector2 oriPos, Vector2 targetPos)
    //{
    //    Vector2 v = targetPos - oriPos;
    //    transform.up = v;
    //}

}

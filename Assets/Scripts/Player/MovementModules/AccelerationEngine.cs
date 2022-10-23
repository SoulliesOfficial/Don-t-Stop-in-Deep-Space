using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEngine : PlayerMovementModule
{
    public float speed;
    public float maxSpeed;
    public Vector3 vectorSpeed;

    public float acceleartion;

    public Vector2 playerPosition, targetPosition, centerPosition, direction, rawDirection;


    private void Start()
    {
        speed = 10;
        maxSpeed = 20;
        acceleartion = 5;
        player = GameManager.playerInputManager.player;
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            player.GetComponent<Rigidbody2D>().velocity *= maxSpeed / player.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
    }

    public override void Move()
    {
        if (player != null)
        {
            playerPosition = GameManager.playerInputManager.player.transform.position;
            targetPosition = GameManager.playerInputManager.mousePosition;


            centerPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (player.isInMovingOrder)
            {
                rawDirection = targetPosition - centerPosition;
                direction = rawDirection.normalized;
                player.transform.up = Vector3.Lerp(player.transform.up, direction, 0.25f);
                player.GetComponent<Rigidbody2D>().AddForce(player.transform.up * speed);
            }

            GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerMovement = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
    }

    public override void Stop()
    {
        speed = 0;
        player.isInMovingOrder = false;
    }
}

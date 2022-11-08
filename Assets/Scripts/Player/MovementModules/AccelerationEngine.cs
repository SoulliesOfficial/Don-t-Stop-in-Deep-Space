using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEngine : PlayerMovementModule
{
    public float pushForce;
    public float maxSpeed;
    public Vector3 vectorSpeed;

    public float acceleartion;

    public Vector2 playerPosition, targetPosition, centerPosition, direction, rawDirection;

    public bool isOverloading;

    private void Start()
    {
        pushForce = 600;
        maxSpeed = 50;
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
                player.transform.up = Vector3.Lerp(player.transform.up, direction, 0.5f);
                player.GetComponent<Rigidbody2D>().AddForce(player.transform.up * pushForce * Time.fixedDeltaTime);
            }

            GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerMovement = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
    }

    public override void MoveAssistance()
    {
        Rigidbody2D p2d = player.GetComponent<Rigidbody2D>();
        p2d.velocity = Vector2.Lerp(p2d.velocity, Vector2.zero, pushForce * 0.001f * Time.fixedDeltaTime);
    }
}

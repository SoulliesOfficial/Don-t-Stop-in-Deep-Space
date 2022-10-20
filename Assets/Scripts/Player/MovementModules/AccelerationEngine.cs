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
        speed = 0;
        maxSpeed = 20;
        acceleartion = 5;
        player = GameManager.playerInputManager.player;
    }

    public override void Move()
    {
        playerPosition = GameManager.playerInputManager.player.transform.position;
        targetPosition = GameManager.playerInputManager.mousePosition;


        centerPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (player.isInMovingOrder)
        {
            rawDirection = targetPosition - centerPosition;
            direction = rawDirection.normalized;
            player.transform.up = Vector3.Lerp(player.transform.up, direction, 0.025f);

            speed += acceleartion * Time.deltaTime;
        }
        else
        {
            speed -= acceleartion * Time.deltaTime;
        }

        speed = Mathf.Max(0, speed);
        speed = Mathf.Min(speed, maxSpeed);

        player.transform.Translate(Vector3.up * speed * Time.deltaTime);

        GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerMovement = this.speed;
    }

    public override void Stop()
    {
        speed = 0;
        player.isInMovingOrder = false;
    }
}

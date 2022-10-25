using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ImperialCorvette : Enemy
{
    public Player player;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAt(transform.position, player.transform.position);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*10);
    }

    private void LookAt(Vector2 oriPos, Vector2 targetPos)
    {
        Vector2 v = targetPos - oriPos;
        transform.up = v;
    }

}

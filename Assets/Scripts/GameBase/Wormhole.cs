using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public SpaceRoom fromRoom, toRoom;
    public Wormhole toHole;
    public Vector2 fromPosition, toPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = toRoom.roomCenter + toPosition;
            toHole.GetComponent<Collider2D>().enabled = false;
        }
    }
}

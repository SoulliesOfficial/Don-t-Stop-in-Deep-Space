using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UniRx;
public class Wormhole : MonoBehaviour
{
    public SpaceRoom fromRoom, toRoom;
    public Wormhole toHole;
    public Vector2 fromPosition, toPosition;

    public GameObject barrier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = toRoom.roomCenter + toPosition;
            collision.gameObject.GetComponent<Player>().trail.gameObject.SetActive(false);
            Observable.Timer(System.TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                collision.gameObject.GetComponent<Player>().trail.gameObject.SetActive(true);
            });
            //toHole.GetComponent<Collider2D>().enabled = false;
        }
    }

    public static void GenerateWormhole(SpaceRoom fromRoom, Vector2 fromPosition, SpaceRoom toRoom, Vector2 toPosition)
    {
        Wormhole wormhole = LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.wormhole).GetComponent<Wormhole>();
        Vector2 exactFromPosition = fromRoom.roomCenter + fromPosition;
        Vector2 exactToPosition = toRoom.roomCenter + toPosition;
        wormhole.fromRoom = fromRoom;
        wormhole.toRoom = toRoom;
        wormhole.fromPosition = fromPosition;
        wormhole.toPosition = toPosition;
        wormhole.transform.position = exactFromPosition;

        wormhole.GetComponent<SpriteRenderer>().color = Color.gray;
        wormhole.GetComponent<BoxCollider2D>().enabled = false;
        fromRoom.wormholes.Add(wormhole);

    }
}

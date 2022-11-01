using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Lean.Pool;
using UniRx;
using System.Linq;

public class SpaceRoom : MonoBehaviour
{
    public Vector2 roomCenter;

    public Vector2[] maximumBorder;
    public List<RoomBorder> roomBorders;
    public List<Enemy_Save> enemieSaves;
    public Vector2 playerSpawnPosition;
    public List<Vector2> wormholePositions;

    public List<Wormhole> wormholes;
    public List<Enemy> enemies;
    public List<PolygonColliderGenerator> roomBorderCollider;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void GenerateSpaceRoom()
    {
        for(int i = 0; i < roomBorders.Count; i++)
        {
            SplineComputer rbt = Instantiate(GameManager.gameManager.gameBasePrefabs.roomBorderTrack, transform).GetComponent<SplineComputer>();
            PolygonColliderGenerator pcg = rbt.gameObject.AddComponent<PolygonColliderGenerator>();
            roomBorderCollider.Add(pcg);
            rbt.is2D = true;
            for (int j = 0; j < roomBorders[i].nodeList.Count; j++)
            {
                rbt.SetPoint(j, new SplinePoint() { position = roomCenter + roomBorders[i].nodeList[j], size = 1f, color = Color.white });
            }

            if (roomBorders[i].isClosed)
            {
                rbt.Close();
            }
            rbt.RebuildImmediate();
            pcg.spline = rbt;
        }

        for (int i = 0; i < enemieSaves.Count; i++)
        {
            Enemy.GenerateEnemy(enemieSaves[i].enemyName, this, enemieSaves[i].position);
        }
    }

    public static SpaceRoom GenerateSpaceRoom(SpaceRoom_Save spaceRoom_Save, Vector2 roomCenter)
    {
        SpaceRoom sr = LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.spaceRoom).GetComponent<SpaceRoom>();
        sr.roomCenter = roomCenter;
        sr.transform.position = roomCenter;
        sr.maximumBorder = spaceRoom_Save.maximumBorder;
        sr.playerSpawnPosition = spaceRoom_Save.playerSpawnPosition;
        sr.wormholePositions = spaceRoom_Save.possibleWormholePositions.ToList();
        sr.roomBorders = new List<RoomBorder>();
        sr.enemieSaves = new List<Enemy_Save>();
        sr.enemies = new List<Enemy>();
        for (int i = 0; i < spaceRoom_Save.roomBorders.Count; i++)
        {
            SplineComputer rbt = LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.roomBorderTrack, sr.transform).GetComponent<SplineComputer>();
            PolygonColliderGenerator pcg = rbt.gameObject.AddComponent<PolygonColliderGenerator>();
            sr.roomBorderCollider.Add(pcg);
            rbt.is2D = true;
            for (int j = 0; j < spaceRoom_Save.roomBorders[i].nodeList.Count; j++)
            {
                rbt.SetPoint(j, new SplinePoint() { position = roomCenter + spaceRoom_Save.roomBorders[i].nodeList[j], size = 1f, color = Color.white });
            }

            if(spaceRoom_Save.roomBorders[i].borderShapeType == RoomBorder.BorderShapeType.Linear)
            {
                rbt.type = Spline.Type.Linear;
            }
            else if(spaceRoom_Save.roomBorders[i].borderShapeType == RoomBorder.BorderShapeType.CatmullRom)
            {
                rbt.type = Spline.Type.CatmullRom;
            }

            if (spaceRoom_Save.roomBorders[i].isClosed)
            {
                rbt.Close();
            }
            rbt.RebuildImmediate();
            pcg.spline = rbt;
            sr.roomBorders.Add(spaceRoom_Save.roomBorders[i]);
        }

        for (int i = 0; i < spaceRoom_Save.enemies.Count; i++)
        {
            Enemy e = Enemy.GenerateEnemy(spaceRoom_Save.enemies[i].enemyName, sr, spaceRoom_Save.enemies[i].position);
            sr.enemieSaves.Add(spaceRoom_Save.enemies[i]);
            sr.enemies.Add(e);
        }
        return sr;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.Reflect(collision.relativeVelocity * -1, collision.contacts[0].normal);
        //collision.gameObject.GetComponent<Player>().nowUsingMovementModule.GetComponent<AccelerationEngine>().speed = 0;
        //collision.gameObject.GetComponent<Player>().extraDirection = (collision.gameObject.GetComponent<Player>().playerPosition - collision.collider.ClosestPoint(transform.position)).normalized;
        //collision.gameObject.GetComponent<Player>().extraSpeed = collision.gameObject.GetComponent<Player>().instantSpeed;
    }
}

public class SpaceRoom_Save : IComparer<SpaceRoom_Save>
{
    public Vector2[] maximumBorder = new Vector2[2];
    public List<RoomBorder> roomBorders;
    public List<Enemy_Save> enemies;

    public Vector2 playerSpawnPosition;
    public Vector2[] possibleWormholePositions;
    public int order;

    public SpaceRoom_Save() { }

    public SpaceRoom_Save(Vector2[] maximumBorder, List<RoomBorder> roomBorders, List<Enemy_Save> enemies,Vector2 playerSpawnPosition, Vector2[]possibleWormholePositions, int order)
    {
        this.maximumBorder = maximumBorder;
        this.roomBorders = roomBorders;
        this.enemies = enemies;
        this.playerSpawnPosition = playerSpawnPosition;
        this.possibleWormholePositions = possibleWormholePositions;
        this.order = order;
    }

    public int Compare(SpaceRoom_Save x, SpaceRoom_Save y)
    {
        return x.order.CompareTo(y.order);
    }
}

[System.Serializable]
public class RoomBorder
{
    public List<Vector2> nodeList;
    public enum BorderShapeType { Linear, CatmullRom }
    public BorderShapeType borderShapeType;
    public bool isClosed;

    public RoomBorder() { }

    public RoomBorder(List<Vector2> nodeList, BorderShapeType borderShapeType, bool isClosed)
    {
        this.nodeList = nodeList;
        this.borderShapeType = borderShapeType;
        this.isClosed = isClosed;
    }
}

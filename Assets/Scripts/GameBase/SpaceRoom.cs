using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Lean.Pool;
using UniRx;
using System.Linq;

public class SpaceRoom : MonoBehaviour
{
    public List<PolygonColliderGenerator> roomBorderCollider;

    public Vector2 roomCenter;
    public Vector2[] maximumBorder;
    public List<RoomBorder> roomBorders;
    public List<Enemy_Save> enemies;



    // Start is called before the first frame update
    void Start()
    {
        //roomCenter = transform.position;
        //roomBorderCollider = new List<PolygonColliderGenerator>();
        //roomBorders = new List<RoomBorder>();
        //maximumBorder = new Vector2[2] {new Vector2(-50, 50) * 1.1f, new Vector2(50, -50) * 1.1f };
        //roomBorders.Add(new RoomBorder(new List<Vector2>() { new Vector2(-50, -50), new Vector2(-50, 50), new Vector2(50, 50), new Vector2(50, -50) }, RoomBorder.BorderShapeType.CatmullRom, true));



        Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            //GenerateSpaceRoom();
            //List<SpaceRoom_Save> rooms = new List<SpaceRoom_Save>();
            //rooms.Add(new SpaceRoom_Save(this.maximumBorder, this.roomBorders, this.enemies));
            //ES3.Save("SpaceRooms", rooms, new ES3Settings(Application.streamingAssetsPath + "/Rooms.txt"));
        });
    }

    public void GenerateSpaceRoom()
    {
        for(int i = 0; i < roomBorders.Count; i++)
        {
            SplineComputer rbt = Instantiate(GameManager.gameManager.gameBasePrefabs.roomBorderTrack, transform).GetComponent<SplineComputer>();
            PolygonColliderGenerator pcg = gameObject.AddComponent<PolygonColliderGenerator>();
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

        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy.GenerateEnemy(enemies[i].enemyName, this, enemies[i].position);
        }
    }

    public static SpaceRoom GenerateSpaceRoom(SpaceRoom_Save spaceRoom_Save, Vector2 roomCenter)
    {
        SpaceRoom sr = LeanPool.Spawn(GameManager.gameManager.gameBasePrefabs.spaceRoom).GetComponent<SpaceRoom>();
        sr.roomCenter = roomCenter;
        sr.transform.position = roomCenter;
        sr.maximumBorder = spaceRoom_Save.maximumBorder;

        for (int i = 0; i < spaceRoom_Save.roomBorders.Count; i++)
        {
            SplineComputer rbt = Instantiate(GameManager.gameManager.gameBasePrefabs.roomBorderTrack, sr.transform).GetComponent<SplineComputer>();
            PolygonColliderGenerator pcg = sr.gameObject.AddComponent<PolygonColliderGenerator>();
            sr.roomBorderCollider.Add(pcg);
            rbt.is2D = true;
            for (int j = 0; j < spaceRoom_Save.roomBorders[i].nodeList.Count; j++)
            {
                rbt.SetPoint(j, new SplinePoint() { position = roomCenter + spaceRoom_Save.roomBorders[i].nodeList[j], size = 1f, color = Color.white });
            }

            if (spaceRoom_Save.roomBorders[i].isClosed)
            {
                rbt.Close();
            }
            rbt.RebuildImmediate();
            pcg.spline = rbt;
        }

        for(int i = 0; i < spaceRoom_Save.enemies.Count; i++)
        {
            Enemy.GenerateEnemy(spaceRoom_Save.enemies[i].enemyName, sr, spaceRoom_Save.enemies[i].position);
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

public class SpaceRoom_Save
{
    public Vector2[] maximumBorder = new Vector2[2];
    public List<RoomBorder> roomBorders;
    public List<Enemy_Save> enemies;

    public SpaceRoom_Save() { }

    public SpaceRoom_Save(Vector2[] maximumBorder, List<RoomBorder> roomBorders, List<Enemy_Save> enemies)
    {
        this.maximumBorder = maximumBorder;
        this.roomBorders = roomBorders;
        this.enemies = enemies;
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

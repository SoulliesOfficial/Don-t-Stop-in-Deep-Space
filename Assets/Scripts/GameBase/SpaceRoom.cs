using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Lean.Pool;
using UniRx;
using System.Linq;

public class SpaceRoom : MonoBehaviour
{
    public GameObject roomBorderTrack;
    public List<BoxCollider2D> roomBorderCollider;
    public List<Vector2> colliderPoints;

    public Vector2 roomCenter;
    public Vector2[] maximumBorder;
    public List<RoomBorder> roomBorders;



    // Start is called before the first frame update
    void Start()
    {
        roomCenter = transform.position;
        roomBorders = new List<RoomBorder>();
        maximumBorder = new Vector2[4] { new Vector2(-50, -50) * 1.1f, new Vector2(-50, 50) * 1.1f, new Vector2(50, 50) * 1.1f, new Vector2(50, -50) * 1.1f };
        roomBorders.Add(new RoomBorder(new List<Vector2>() { new Vector2(-50, -50), new Vector2(-50, 50), new Vector2(50, 50), new Vector2(50, -50) }, true));



        Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            GenerateSpaceRoom();
        });
    }

    public void GenerateSpaceRoom()
    {
        for(int i = 0; i < roomBorders.Count; i++)
        {
            SplineComputer rbt = Instantiate(roomBorderTrack, transform).GetComponent<SplineComputer>();
            rbt.is2D = true;
            for (int j = 0; j < roomBorders[i].nodeList.Count; j++)
            {
                rbt.SetPoint(j, new SplinePoint() { position = roomCenter + roomBorders[i].nodeList[j], size = 0.2f, color = Color.white });
                colliderPoints.Add(roomBorders[i].nodeList[j]);
            }

            if (roomBorders[i].isClosed)
            {
                rbt.Close();
            }
            rbt.RebuildImmediate();
        }
        roomBorderCollider.points = colliderPoints.Distinct().ToArray();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Player>().nowUsingMovementModule.GetComponent<AccelerationEngine>().speed = 0;
    }
}

[System.Serializable]
public class RoomBorder
{
    public List<Vector2> nodeList;
    public bool isClosed;

    public RoomBorder(List<Vector2> nodeList, bool isClosed)
    {
        this.nodeList = nodeList;
        this.isClosed = isClosed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMap : MonoBehaviour
{

	public List<SpaceRoom_Save> spaceRoomStorage;
    public List<SpaceRoom> spaceRooms;
    public List<Wormhole> wormholes;

    void Start()
    {
		spaceRoomStorage = ES3.Load<List<SpaceRoom_Save>>("SpaceRooms", new ES3Settings(Application.streamingAssetsPath + "/Rooms.txt"));
		GenerateMap();
	}

	void GenerateMap()
	{
		spaceRoomStorage.Sort((SpaceRoom_Save x, SpaceRoom_Save y) => (x.order.CompareTo(y.order)));
		Vector2 newRoomCenter = new Vector2(0,0);
		SpaceRoom_Save newRoom = spaceRoomStorage[0];
		SpaceRoom sr = SpaceRoom.GenerateSpaceRoom(newRoom, newRoomCenter);
		spaceRooms.Add(sr);
		GameManager.player.transform.position = sr.playerSpawnPosition;

        for (int i = 1; i < spaceRoomStorage.Count; i++)
        {
            newRoomCenter = new Vector2(Random.Range(-300, 300), Random.Range(-300, 300));
            newRoom = spaceRoomStorage[i];
            if (IsValidRoom(newRoomCenter, new Vector2[] { newRoom.maximumBorder[0], newRoom.maximumBorder[1] }))
            {
                sr = SpaceRoom.GenerateSpaceRoom(newRoom, newRoomCenter);
                spaceRooms.Add(sr);
				Wormhole.GenerateWormhole(spaceRooms[i - 1], spaceRooms[i - 1].wormholePositions[0], sr, sr.playerSpawnPosition);
			}
			else
            {
                i--;
            }
        }
    }

    public bool IsValidRoom(Vector2 newRoomCenter, Vector2[] newRoomMaximumBorders)
	{
		float r1Lenght, r1height;
		float r2Lenght, r2height;
		for (int i = 0; i < spaceRooms.Count; i++)
		{
			r1Lenght = Mathf.Abs(newRoomMaximumBorders[0].x - newRoomMaximumBorders[1].x);
			r1height = Mathf.Abs(newRoomMaximumBorders[0].y - newRoomMaximumBorders[1].y);
			r2Lenght = Mathf.Abs(spaceRooms[i].maximumBorder[0].x - spaceRooms[i].maximumBorder[1].x);
			r2height = Mathf.Abs(spaceRooms[i].maximumBorder[0].y - spaceRooms[i].maximumBorder[1].y);

			if (Mathf.Abs(newRoomCenter.x - spaceRooms[i].roomCenter.x) < (r1Lenght + r2Lenght) / 2 &&
				Mathf.Abs(newRoomCenter.y - spaceRooms[i].roomCenter.y) < (r1height + r2height) / 2)
			{
				return false;
			}
		}
		return true;
	}
}
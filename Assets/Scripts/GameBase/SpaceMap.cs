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
		Vector2 newRoomCenter = new Vector2(0,0);
		SpaceRoom_Save newRoom = spaceRoomStorage[Random.Range(0, spaceRoomStorage.Count)];
		SpaceRoom sr = SpaceRoom.GenerateSpaceRoom(newRoom, newRoomCenter);
		spaceRooms.Add(sr);

		for (int i = 0; i < Random.Range(6, 12); i++)
		{
			newRoomCenter = new Vector2(Random.Range(-200, 200), Random.Range(-200, 200));
			newRoom = spaceRoomStorage[Random.Range(0, spaceRoomStorage.Count)];
			if (IsValidRoom(newRoomCenter, new Vector2[] { newRoom.maximumBorder[0], newRoom.maximumBorder[1] }))
			{
				sr = SpaceRoom.GenerateSpaceRoom(newRoom, newRoomCenter);
				spaceRooms.Add(sr);
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

	public static bool IsOverlapping(Vector2Int oneMin, Vector2Int oneMax, Vector2Int twoMin, Vector2Int twoMax)
    {
        return Mathf.Max(oneMin.x, twoMin.x) <= Mathf.Min(oneMax.x, twoMax.x) &&
               Mathf.Max(oneMin.y, twoMin.y) <= Mathf.Min(oneMax.y, twoMax.y);
    }
}
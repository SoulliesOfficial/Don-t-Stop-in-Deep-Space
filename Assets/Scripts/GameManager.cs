using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static SubspaceDisruptionSystem subspaceDisruptionSystem;
    public static PlayerInputManager playerInputManager;
    public static Player player;
    public static SpaceMap spaceMap;

    public GameBasePrefabs gameBasePrefabs;

    public Dictionary<string, GameObject> enemyDictionary;

    void Awake()
    {
        Application.targetFrameRate = 120;
        gameManager = this;
        subspaceDisruptionSystem = this.GetComponent<SubspaceDisruptionSystem>();
        playerInputManager = this.GetComponent<PlayerInputManager>();
        player = playerInputManager.player;
        spaceMap = this.GetComponent<SpaceMap>();

        enemyDictionary = new Dictionary<string, GameObject>();
        enemyDictionary.Add("ImperialCorvette", gameBasePrefabs.imperialCorvette);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class GameBasePrefabs
{
    public GameObject spaceRoom;
    public GameObject roomBorderTrack;

    public GameObject imperialCorvette;
}

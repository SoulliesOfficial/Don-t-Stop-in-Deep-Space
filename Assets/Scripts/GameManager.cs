using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static UIManager uiManager;
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
        uiManager = this.GetComponent<UIManager>();
        subspaceDisruptionSystem = this.GetComponent<SubspaceDisruptionSystem>();
        playerInputManager = this.GetComponent<PlayerInputManager>();
        player = playerInputManager.player;
        spaceMap = this.GetComponent<SpaceMap>();

        enemyDictionary = new Dictionary<string, GameObject>();
        enemyDictionary.Add("ImperialCorvette", gameBasePrefabs.imperialCorvette);
        enemyDictionary.Add("ImperialDestroyer", gameBasePrefabs.imperialDestroyer);
        enemyDictionary.Add("BlackHole", gameBasePrefabs.blackHole);
        enemyDictionary.Add("ImperialFortress", gameBasePrefabs.imperialFortress);
        enemyDictionary.Add("EnemySpawnPoint", gameBasePrefabs.enemySpawnPoint);
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
    public GameObject wormhole;

    public GameObject imperialCorvette;
    public GameObject imperialDestroyer;
    public GameObject imperialFortress;

    public GameObject enemySpawnPoint;

    public GameObject blackHole;

}

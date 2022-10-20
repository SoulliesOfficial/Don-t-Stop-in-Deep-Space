using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static SubspaceDisruptionSystem subspaceDisruptionSystem;
    public static PlayerInputManager playerInputManager;
    public static SpaceMap spaceMap;

    void Awake()
    {
        Application.targetFrameRate = 120;
        gameManager = this;
        subspaceDisruptionSystem = this.GetComponent<SubspaceDisruptionSystem>();
        playerInputManager = this.GetComponent<PlayerInputManager>();
        spaceMap = this.GetComponent<SpaceMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

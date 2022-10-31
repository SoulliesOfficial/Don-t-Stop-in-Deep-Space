using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Player player;

    public Vector2 mousePosition;

    public KeyCode shootKey, switchWeaponKey;
    public KeyCode moveKey;
    public KeyCode moveAssistanceKey;
    public KeyCode useFunctionalModuleKey, switchFunctionMuduleKey;

    void Start()
    {
        moveKey = KeyCode.Mouse0;
        moveAssistanceKey = KeyCode.Mouse1;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(moveKey))
        {
            player.isInMovingOrder = true;
        }
        else if(Input.GetKeyUp(moveKey))
        {
            player.isInMovingOrder = false;
        }

        if (Input.GetKeyDown(moveAssistanceKey))
        {
            player.nowUsingMovementModule.MoveAssistance();
        }

        if (Input.GetKeyDown(shootKey))
        {
            player.isShooting = true;
        }
        else if (Input.GetKeyUp(shootKey))
        {
            player.isShooting = false;
        }

    }
}

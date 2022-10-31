using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementModule : MonoBehaviour
{
    public Player player;
    public abstract void Move();
    public abstract void MoveAssistance();
}

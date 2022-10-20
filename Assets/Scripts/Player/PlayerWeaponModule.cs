using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeaponModule : MonoBehaviour
{
    public Player player;
    public abstract void Shoot();
}

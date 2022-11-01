using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class MissileLauncher : PlayerWeaponModule
{
    public float coolDownInterval = 1f;
    public float coolDown = 0f;
    public GameObject missileBullet;

    private void Start()
    {
        player = GameManager.playerInputManager.player;
    }

    private void FixedUpdate()
    {
        coolDown += Time.fixedDeltaTime;
        if (player.nowUsingWeaponModule != null)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (player.isShooting && coolDown >= coolDownInterval)
        {
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 20 && player.energy >= 20f)
            {
                coolDownInterval = 1f;
                LeanPool.Spawn(missileBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<Missile>().Initialize(player.transform.up, 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 5f;
                player.energy -= 20f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue >= 20 && player.energy >= 20f)
            {
                coolDownInterval = 0.5f;
                LeanPool.Spawn(missileBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<Missile>().Initialize(player.transform.up, 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 5f;
                player.energy -= 20f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class MissleLauncher : PlayerWeaponModule
{
    public float coolDownInterval = 1f;
    public float coolDown = 0f;
    public GameObject missileBullet;

    private void Start()
    {
        player = GameManager.playerInputManager.player;
        Time.maximumDeltaTime = 0.02f;
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
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 15)
            {
                coolDownInterval = 1f;
                LeanPool.Spawn(missileBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<Missile>().Initialize(player.transform.up, 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 5f;
                player.energy -= 10f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 50)
            {
                coolDownInterval = 0.5f;
                LeanPool.Spawn(missileBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<Missile>().Initialize(player.transform.up, 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 5f;
                player.energy -= 10f;
            }
        }
    }
}

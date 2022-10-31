using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class LightLanceConcentrator : PlayerWeaponModule
{
    public float coolDownInterval = 2f;
    public float coolDown = 0f;
    public GameObject lightLance;
    public float damage;

    private void Start()
    {
        player = GameManager.playerInputManager.player;
        damage = 10;
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
            Vector2 direction = player.transform.up;
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 15)
            {
                LeanPool.Spawn(lightLance, transform.position, Quaternion.Euler(player.transform.eulerAngles))
                    .GetComponent<LightLance>().Initialize(transform.position, player.transform.up);
                coolDown = 0;
                damage = 10;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 50f;
                player.energy -= 25f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 50)
            {
                LeanPool.Spawn(lightLance, transform.position, Quaternion.Euler(player.transform.eulerAngles))
                    .GetComponent<LightLance>().Initialize(transform.position, player.transform.up);
                damage = 20;
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 50f;
                player.energy -= 50f;
            }
        }
    }
}

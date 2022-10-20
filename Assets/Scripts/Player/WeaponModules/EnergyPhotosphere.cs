using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class EnergyPhotosphere : PlayerWeaponModule
{
    public const float coolDownInterval = 0.2f;
    public float coolDown = 0f;
    public GameObject photosphereBullet;

    private void Start()
    {
        player = GameManager.playerInputManager.player;
    }

    private void Update()
    {
        coolDown += Time.deltaTime;
    }

    public override void Shoot()
    {
        if (player.isShooting && coolDown >= coolDownInterval)
        {
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 25)
            {
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.localEulerAngles)).GetComponent<PhotosphereBullet>().Initialize(player.transform.up, 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 1f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 50)
            {
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.localEulerAngles)).GetComponent<PhotosphereBullet>().Initialize(player.transform.up, 10f + player.instantSpeed);
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.localEulerAngles + new Vector3(0, 0, 5))).GetComponent<PhotosphereBullet>().Initialize(RotateVector(player.transform.up, 5), 10f + player.instantSpeed);
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.localEulerAngles + new Vector3(0, 0, -5))).GetComponent<PhotosphereBullet>().Initialize(RotateVector(player.transform.up, -5), 10f + player.instantSpeed);
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 1f;
            }

        }
    }

    public Vector2 RotateVector(Vector2 origin, float angle)
    {
        return new Vector2(origin.x * Mathf.Cos(angle) + origin.y * Mathf.Sin(angle), -origin.x * Mathf.Sin(angle) + origin.y * Mathf.Cos(angle));
    }

}

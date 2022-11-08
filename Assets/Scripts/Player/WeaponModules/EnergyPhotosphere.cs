using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class EnergyPhotosphere : PlayerWeaponModule
{
    //
    AudioSource sfxPlayer;



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
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 20)
            {
                //
                sfxPlayer = GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().sfxPlayer;
                sfxPlayer.PlayOneShot(GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().p_shoot);

                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<PhotosphereBullet>().Initialize(player.transform.up, 10f);
                coolDown = 0f;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 1f;
                player.energy -= 2f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue >= 20)
            {
                //
                sfxPlayer = GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().sfxPlayer;
                sfxPlayer.PlayOneShot(GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().p_shoot);

                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles)).GetComponent<PhotosphereBullet>().Initialize(player.transform.up, 10f + player.instantSpeed);
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 10))).GetComponent<PhotosphereBullet>().Initialize(RotateVector(player.transform.up, 10), 10f);
                LeanPool.Spawn(photosphereBullet, transform.position, Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, -10))).GetComponent<PhotosphereBullet>().Initialize(RotateVector(player.transform.up, -10), 10f);
                coolDown = 0f;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 3f;
                player.energy -= 6f;
            }
            else
            {
                GameManager.uiManager.shipUI.StartShake();
            }
        }
    }

    public Vector2 RotateVector(Vector2 origin, float angle)
    {
        return new Vector2
            (origin.x * Mathf.Cos(angle * Mathf.Deg2Rad) + origin.y * Mathf.Sin(angle * Mathf.Deg2Rad),
            -origin.x * Mathf.Sin(angle * Mathf.Deg2Rad) + origin.y * Mathf.Cos(angle * Mathf.Deg2Rad));
    }

}

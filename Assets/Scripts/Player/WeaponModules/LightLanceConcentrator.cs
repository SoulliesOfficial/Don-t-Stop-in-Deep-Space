using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class LightLanceConcentrator : PlayerWeaponModule
{
    //
    public AudioSource sfxPlayer;

    public float coolDownInterval = 2f;
    public float coolDown = 0f;
    public GameObject lightLance;
    public float damage;

    private void Start()
    {
        player = GameManager.playerInputManager.player;
        damage = 10;
    }

    private void Update()
    {
        coolDown += Time.deltaTime;
    }

    public override void Shoot()
    {
        if (player.isShooting && coolDown >= coolDownInterval)
        {
            Vector2 direction = player.transform.up;
            if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue <= 20 && player.energy >= 33f)
            {
                //
                sfxPlayer = GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().sfxPlayer;
                sfxPlayer.PlayOneShot(GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().p_laser);

                LeanPool.Spawn(lightLance, transform.position, Quaternion.Euler(player.transform.eulerAngles))
                    .GetComponent<LightLance>().Initialize(transform.position, player.transform.up, 10);
                coolDown = 0;
                damage = 10;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 50f;
                player.energy -= 33f;
            }
            else if (GameManager.subspaceDisruptionSystem.subspaceDisruptionValue >= 20 && player.energy>=66f)
            {
                //
                sfxPlayer = GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().sfxPlayer;
                sfxPlayer.PlayOneShot(GameObject.FindWithTag("PlayerAudio").GetComponent<AudioManager>().p_laser);

                LeanPool.Spawn(lightLance, transform.position, Quaternion.Euler(player.transform.eulerAngles))
                    .GetComponent<LightLance>().Initialize(transform.position, player.transform.up, 20);
                damage = 20;
                coolDown = 0;
                GameManager.subspaceDisruptionSystem.subspaceDisruptionValueParts.playerAttackIntensity += 50f;
                player.energy -= 66f;
            }
            else
            {
                GameManager.uiManager.shipUI.StartShake();
            }

        }
    }
}

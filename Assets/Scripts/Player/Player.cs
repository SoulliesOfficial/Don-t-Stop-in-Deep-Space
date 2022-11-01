using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerWeaponModule> weaponModuleList;
    public PlayerWeaponModule nowUsingWeaponModule;

    public List<PlayerMovementModule> movementModuleList;
    public PlayerMovementModule  nowUsingMovementModule;

    public List<PlayerFunctionalModule> functionalModuleList;
    public PlayerWeaponModule nowUsingFunctionMudules;

    public List<PlayerPassiveModule> passiveModuleList;

    public bool isInMovingOrder, isShooting;

    public GameObject weaponModule, movementModule, functionalModule, passiveModule;
    public Vector2 playerPosition;

    public float life, energy;
    public float energyRecover;

    private Vector2 lastPos, nowPos;
    public Vector2 displacement;
    public float instantSpeed;

    public float invincibleTime;

    void Start()
    {
        isInMovingOrder = false;
        isShooting = false;
        life = 100;
        energy = 100;
        energyRecover = 5f;

        InstallModule("movement", "AcclerationEngine");
        //InstallModule("weapon", "LightLanceConcentrator");
        InstallModule("weapon", "MissileLauncher");

        this.nowUsingMovementModule = movementModuleList[0];
        this.nowUsingWeaponModule = weaponModuleList[0];
    }

    private void FixedUpdate()
    {
        if (nowUsingMovementModule != null)
        {
            nowUsingMovementModule.Move();
        }

        energy += energyRecover * Time.fixedDeltaTime;
        energy = Mathf.Min(energy, 100f);
        playerPosition = transform.position;

        nowPos = transform.position;
        displacement = nowPos - lastPos;
        instantSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPos = nowPos;
    }

    void Update()
    {
        invincibleTime -= Time.deltaTime;
    }

    void InstallModule(string moduleType, string moduleName)
    {
        GameObject mod = Instantiate(Resources.Load(moduleName)) as GameObject;

        if(moduleType == "weapon")
        {
            mod.transform.SetParent(weaponModule.transform);
            this.weaponModuleList.Add(mod.GetComponent<PlayerWeaponModule>());
        }
        else if(moduleType == "movement")
        {
            mod.transform.SetParent(movementModule.transform);
            this.movementModuleList.Add(mod.GetComponent<PlayerMovementModule>());
        }
        else if (moduleType == "functional")
        {
            mod.transform.SetParent(functionalModule.transform);
            this.functionalModuleList.Add(mod.GetComponent<PlayerFunctionalModule>());
        }
        else if (moduleType == "passive")
        {
            mod.transform.SetParent(passiveModule.transform);
            this.passiveModuleList.Add(mod.GetComponent<PlayerPassiveModule>());
        }

        mod.transform.localPosition = Vector3.zero;
        mod.transform.localEulerAngles = Vector3.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (invincibleTime < 0) {
            if (collision.gameObject.tag == "Enemy")
            {
                life -= 25;
                invincibleTime = 3f;
            }

            if (collision.gameObject.tag == "EnemyBullet")
            {
                life -= 25;
                invincibleTime = 3f;
                Destroy(collision.gameObject);
            }
        }
    }

}
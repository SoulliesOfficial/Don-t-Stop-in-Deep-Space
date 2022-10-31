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

    void Start()
    {
        isInMovingOrder = false;
        isShooting = false;
        life = 100;
        energy = 100;
        energyRecover = 1;

        InstallModule("movement", "AcclerationEngine");
        InstallModule("weapon", "LightLanceConcentrator");

        this.nowUsingMovementModule = movementModuleList[0];
        this.nowUsingWeaponModule = weaponModuleList[0];
    }

    private void FixedUpdate()
    {
        if (nowUsingMovementModule != null)
        {
            nowUsingMovementModule.Move();
        }

        energy = Mathf.Lerp(energy, 100, energyRecover * Time.fixedDeltaTime);

        playerPosition = transform.position;

        nowPos = transform.position;
        displacement = nowPos - lastPos;
        instantSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPos = nowPos;
    }

    void Update()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
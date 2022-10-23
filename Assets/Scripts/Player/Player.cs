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

    private Vector2 lastPos, nowPos;
    public Vector2 displacement;
    public float instantSpeed, extraSpeed, extraAccleration;
    public Vector2 extraDirection;

    void Start()
    {
        isInMovingOrder = false;
        isShooting = false;
        extraSpeed = 0;
        extraAccleration = 5;

        InstallModule("movement", "AcclerationEngine");
        InstallModule("weapon", "EnergyPhotosphere");

        this.nowUsingMovementModule = movementModuleList[0];
        this.nowUsingWeaponModule = weaponModuleList[0];
    }

    private void FixedUpdate()
    {
        if (nowUsingMovementModule != null)
        {
            nowUsingMovementModule.Move();
        }



        playerPosition = transform.position;

        nowPos = transform.position;
        displacement = nowPos - lastPos;
        instantSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPos = nowPos;
    }

    void Update()
    {

        if (nowUsingWeaponModule != null)
        {
            nowUsingWeaponModule.Shoot();
        }

        //if (extraSpeed > 0)
        //{
        //    extraSpeed -= extraAccleration * Time.deltaTime;
        //    extraSpeed = Mathf.Max(0, extraSpeed);
        //}
        //else if(extraSpeed<0)
        //{
        //    extraSpeed += extraAccleration * Time.deltaTime;
        //    extraSpeed = Mathf.Min(0, extraSpeed);
        //}
        //transform.Translate(extraDirection * extraSpeed * Time.deltaTime);
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
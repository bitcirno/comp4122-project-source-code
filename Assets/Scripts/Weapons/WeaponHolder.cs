using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.Managers;

public class WeaponHolder : MonoBehaviour
{
    public GameObject oriWeapon;
    public CrossHair crossHair;

    public GameObject weapon;
    public string weaponName;
    AbstractGun weaponScript;
    // Start is called before the first frame update
    void Start()
    {
        weaponName = oriWeapon.name.Split('(')[0];
        Debug.Log($"weaponname {weaponName}");
        weapon = Instantiate(oriWeapon, transform.position, transform.rotation, transform);
        weaponScript = weapon.GetComponent(typeof(AbstractGun)) as AbstractGun;

        GameObject player=this.transform.root.gameObject;
        IKControl pIK=player.GetComponent(typeof(IKControl)) as IKControl;
        pIK.leftHandObj=weapon.transform.Find("leftHandLocation");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 r = Quaternion.FromToRotation(Vector3.forward, AimCrossHairManager.Instance.targetPoint - transform.position).eulerAngles;
        transform.rotation = Quaternion.Euler(r.x, r.y, 0);



        if (weaponScript.inputReload())
        {
            Debug.Log("WeaponHolder: active reload");
            weaponScript.reload();
        }

        if (weaponScript.inputActivate())
        {
            weaponScript.Activate();
        }
        
    }

    void switchWeapon(GameObject newWeapon)
    {
        Destroy(weapon);

        weapon=Instantiate(newWeapon,transform.position, transform.rotation, transform);
        Rigidbody rigidbody=weapon.GetComponent<Rigidbody>();
        BoxCollider boxCollider=weapon.GetComponent<BoxCollider>();
        Destroy(rigidbody);
        Destroy(boxCollider);

        weaponScript = weapon.GetComponent(typeof(AbstractGun)) as AbstractGun;
        Destroy(newWeapon);

        GameObject player=this.transform.root.gameObject;
        IKControl pIK=player.GetComponent(typeof(IKControl)) as IKControl;
        pIK.leftHandObj=weapon.transform.Find("leftHandLocation");
    }

    public String getWeaponName()
    {
        return weapon.name.Split('(')[0];
    }

    private void OnTriggerEnter(Collider other) {

        GameObject otherObj=other.gameObject;
        var gunScript = other.GetComponent<AbstractGun>();
        if(otherObj.layer==LayerMask.NameToLayer("Weapon")){
            Debug.Log("find a weapon");
            switchWeapon(otherObj);
            AimCrossHairManager.Instance.ChangeCrossHair(gunScript.crossHair);
        }
    }
    
}
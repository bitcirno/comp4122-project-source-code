using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.Managers;

public class EnemyWeaponHolder : MonoBehaviour
{
    public GameObject oriWeapon;

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

    public void Shoot()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponScript.inputActivate())
        {
            weaponScript.Activate();
        }
        Vector3 r = Quaternion.FromToRotation(Vector3.forward, AimCrossHairManager.Instance.targetPoint - transform.position).eulerAngles;
        transform.rotation = Quaternion.Euler(r.x, r.y, 0);
        
        if (weaponScript.inputReload())
        {
            Debug.Log("WeaponHolder: active reload");
            weaponScript.reload();
        }
    }

    void switchWeapon(GameObject newWeapon)
    {

        return;
    }
}

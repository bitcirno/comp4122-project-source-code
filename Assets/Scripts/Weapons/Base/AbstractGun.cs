using System.Collections;
using System.Collections.Generic;
using Managers;
using UIFramework.Managers;
using UnityEngine;

public class AbstractGun : MonoBehaviour
{
    public int ammunition;
    public float fireRate;
    public float reloadTime;
    public GameObject bullet;

    public float bulletDamage;
    public float bulletSpeed;

    protected int currentAmmunition;
    protected float coolDown;
    protected bool reloading;
    protected float currentReloadTime;
    public CrossHair crossHair;

    private Animator playerAnimator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentAmmunition = ammunition;
        reloading = false;
        coolDown = float.MaxValue;
        currentReloadTime = float.MaxValue;

        if (this.transform.root != this.transform)
        {
            playerAnimator = this.transform.root.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("isReloading", false);
        }
    }

    void Update()
    {
        coolDown += Time.deltaTime;
        currentReloadTime += Time.deltaTime;

        if (reloading == true && currentReloadTime >= reloadTime)
        {
            reloading = false;
            playerAnimator.SetBool("isReloading", false);
            Debug.Log("finish reloading");
            currentAmmunition = ammunition;
        }

        PlayerInfo.Instance.setAmmunition(name.Split('(')[0], currentAmmunition, ammunition);
    }


    public virtual void shoot()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        coolDown = 0;
        currentAmmunition--;
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(transform.rotation * Vector3.forward));
        AbstractBullet prototypeBulletScript = currentBullet.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
        prototypeBulletScript.damage = bulletDamage;
        prototypeBulletScript.direction = transform.rotation * Vector3.forward;
        prototypeBulletScript.speed = bulletSpeed;
    }

    public virtual void reload()
    {
        reloading = true;
        currentReloadTime = 0;
        playerAnimator.SetBool("isReloading", true);
    }
    public virtual bool inputActivate()
    {
        return GlobalManager.Instance.IsPlayerAlive() && Input.GetButtonDown("Fire1");
    }

    public virtual bool inputReload()
    {
        return Input.GetButtonDown("Reload"); ;
    }

    public virtual void Activate()
    {

        Debug.Log("activate");



        if (reloading == true)
        {
            Debug.Log("reloading");
            if (currentReloadTime >= reloadTime)
            {
                reloading = false;
                playerAnimator.SetBool("isReloading", false);
                Debug.Log("finish reloading");
                currentAmmunition = ammunition;
            }
        }
        else
        {

            if (currentAmmunition <= 0)
            {
                Debug.Log("start to reload");
                reload();
            }
            else if (coolDown >= 1 / fireRate)
            {
                Debug.Log($"shoot {currentAmmunition - 1} left");
                shoot();
            }
            else
            {
                Debug.Log("cooldowning");
            }
        }
    }

}

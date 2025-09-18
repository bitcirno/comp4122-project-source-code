using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractShotgun : AbstractGun
{
    public int projectile;

    public float maxScatteringRadian;



    Quaternion getScattering(float maxRadian){

        float radian=Random.Range(0,maxRadian);
        float xRotation=Random.Range(0,360);

        Vector3 direction=new Vector3(Mathf.Cos(radian),Mathf.Sin(radian),0);
        direction=Quaternion.Euler(xRotation,0,0)*direction;
        Quaternion scattering=Quaternion.FromToRotation(new Vector3(1,0,0),direction);
        Debug.Log($"radian {radian}, xr {xRotation}, dir {direction}");

        return scattering;
    }

    public override void shoot(){
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        coolDown=0;
        currentAmmunition--;
        for(int i=0;i<projectile;i++){
            Quaternion scattering=getScattering(maxScatteringRadian);
            GameObject currentBullet=Instantiate(bullet,transform.position,Quaternion.LookRotation(scattering*(transform.rotation*Vector3.forward)));
            AbstractBullet prototypeBulletScript=currentBullet.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
            prototypeBulletScript.damage=bulletDamage;
            prototypeBulletScript.direction=(scattering*(transform.rotation*Vector3.forward));
            prototypeBulletScript.speed=bulletSpeed;
        }

    }
}

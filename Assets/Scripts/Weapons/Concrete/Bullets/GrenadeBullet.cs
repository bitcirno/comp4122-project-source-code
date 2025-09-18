using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrenadeBullet : AbstractBullet
{
    
    public GameObject ExplosionRange;
    public Animator _animator;
    public ParticleSystem ExplodeWaitEffect;
    private static readonly int Explode = Animator.StringToHash("explode");


    protected override void OnCollisionEnter(Collision other)
    { 
        ParticleSystem effect = Instantiate(ExplodeWaitEffect,gameObject.transform.position,Quaternion.identity);
        effect.Play();
        Invoke("explode",1.0f);
    }

    
    protected void explode()
    {   
        _animator.SetTrigger(Explode);
        Instantiate(ExplosionRange, transform.position, quaternion.identity);
    }
}
    // void OnCollisionEnter()
    // {
    //     Destroy(gameObject);
    // }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : AbstractBullet
{
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer!=LayerMask.NameToLayer("Enemy"))
        {
            DestoryBullet();
        }
    }
    
}

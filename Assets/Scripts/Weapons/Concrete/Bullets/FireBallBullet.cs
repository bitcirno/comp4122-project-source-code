using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBullet : AbstractBullet
{
    public GameObject explosion;
    public GameObject explosionParticle;
    
    protected override void DestoryBullet()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        base.DestoryBullet();
    }
}

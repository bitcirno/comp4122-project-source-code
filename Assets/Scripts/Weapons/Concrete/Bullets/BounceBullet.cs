using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : AbstractBullet
{
    // Start is called before the first frame update
    protected float BounceForce = 1.0f; // bouncing force
    
    

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 11)
        {
            // collide with obstacle and ground
            BounceBack(other);
        }

    }
    private void BounceBack(Collision other)
    {
        rb.AddForce(Vector3.Reflect(other.GetContact(0).point, other.GetContact(0).normal)*BounceForce);
    }
}

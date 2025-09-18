using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{


    public float damage;
    public float speed;
    public Vector3 direction;
    protected float destorytime = 5.0f;
    protected Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("bullet: create a bullet");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
        Console.WriteLine($"current speed:{rb.velocity.ToString()}");
        Invoke("DestoryBullet",destorytime);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        DestoryBullet();
    }

    protected virtual void DestoryBullet()
    {
        Destroy(this.gameObject);
    }

}

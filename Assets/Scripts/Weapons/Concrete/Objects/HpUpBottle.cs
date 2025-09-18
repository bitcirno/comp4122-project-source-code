using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using Characters.MovingController;

public class HpUpBottle : AbstractBottle
{
    // Start is called before the first frame update
    public int HpUp;
    public float time;
    private string type = "HpUp";
    void Start()
    {
        SetValue(HpUp,time,type);
    }

    private void Update()
    {
        checkCollide();
    }

    public override void ValueUp(Collider other)
    {
       var playerMovingScript= other.GetComponent<Player>();
       playerMovingScript.ExtendHpMaxByValue(HpUp);
       Debug.Log("HpUP!");

    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.MovingController;
using UnityEngine;

public class HealthBottle : AbstractBottle
{
    // Start is called before the first frame update
    public int health;
    public float time;
    private string type = "Hp";
    void Start()
    {
        SetValue(health,time,type);
    }

    // Update is called once per frame
    void Update()
    {
        checkCollide();
    }

    public override void ValueUp(Collider other)
    {
        var playerMovingScript= other.GetComponent<Player>();
        playerMovingScript.Heal(health);
        Debug.Log("recover");
    }


}

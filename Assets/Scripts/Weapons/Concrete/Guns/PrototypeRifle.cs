using System.Collections;
using System.Collections.Generic;
using UIFramework.Managers;
using UnityEngine;

public class PrototypeRifle : AbstractRifle
{
    protected override void Start()
    {
        base.Start();
        crossHair = CrossHair.Rifle;
    }
    
}

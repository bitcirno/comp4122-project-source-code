using System.Collections;
using System.Collections.Generic;
using UIFramework.Managers;
using UnityEngine;

public class PrototypeShotgun : AbstractShotgun
{
    protected override void Start()
    {
        base.Start();
        crossHair = CrossHair.Shotgun;
    }
    
}

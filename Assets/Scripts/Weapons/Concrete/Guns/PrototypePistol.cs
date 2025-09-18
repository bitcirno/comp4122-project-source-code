using System.Collections;
using System.Collections.Generic;
using UIFramework.Managers;
using UnityEngine;

public class PrototypePistol : AbstractPistol
{
    protected override void Start()
    {
        base.Start();
        crossHair = CrossHair.Pistol;
    }
}

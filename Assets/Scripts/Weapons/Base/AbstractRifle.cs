using System.Collections;
using System.Collections.Generic;
using UIFramework.Managers;
using UnityEngine;

public abstract class AbstractRifle : AbstractGun
{

    public override bool inputActivate()
    {
        return Input.GetKey("mouse 0");
    }
}

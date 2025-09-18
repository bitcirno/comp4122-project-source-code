using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : AbstractBullet
{
    void Start()
    {
        destorytime = 0.5f;
        Invoke("DestoryBullet",destorytime);
    }
}

using System;
using Characters;
using UnityEngine;

namespace Weapons.Concrete.Bullets
{
    public class SlimeBullet : AbstractBullet
    {
        protected override void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var script = other.gameObject.GetComponent<Player>();
                script.Hurt((int)damage);
                DestoryBullet();
            }
        }
    }
}
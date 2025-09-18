using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseGun : MonoBehaviour
    {
        public int ammunition;
        public float fireRate;
        public float reloadTime;
        public GameObject bullet;

        public float bulletDamage;
        public float bulletSpeed;

        protected int currentAmmunition;
        protected float coolDown;
        protected bool reloading;
        protected float currentReloadTime;

        // Start is called before the first frame update
        void Start()
        {
            currentAmmunition = ammunition;
            reloading = false;
            coolDown = float.MaxValue;
            currentReloadTime = float.MaxValue;
        }

        public virtual void shoot()
        {
        }
        public void reload()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

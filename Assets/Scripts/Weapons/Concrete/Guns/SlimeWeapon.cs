using UnityEngine;
using UnityEngine.WSA;
using Weapons.Concrete.Bullets;

namespace Weapons.Concrete.Guns
{
    public class SlimeWeapon : MonoBehaviour
    {
        public float cd;
        public GameObject slimeBullet;
        public float damage;
        public float bulletSpeed;

        private float _nextShootingTime;
        private bool _canShoot = true;

        public void Shoot(Transform target)
        {
            Invoke(nameof(ShootBullet), 0.08f);
            _nextShootingTime = Time.time + cd;
            _canShoot = false;
        }

        private void ShootBullet()
        {
            var transform1 = transform;
            var currentBullet = Instantiate(slimeBullet, transform1.position, Quaternion.identity);
            AbstractBullet bulletScript = currentBullet.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
            bulletScript.transform.rotation = transform1.rotation;
            bulletScript.damage = damage;
            bulletScript.direction = transform.rotation * Vector3.forward;
            bulletScript.speed = bulletSpeed;
        }
        
        private void Update()
        {
            if (_canShoot) return;
            if (Time.time > _nextShootingTime)
            {
                _canShoot = true;
            }
        }

        public bool CanShoot()
        {
            return _canShoot;
        }
    }
}
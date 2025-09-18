using System;
using System.Collections.Generic;
using Characters.MovingController;
using UnityEngine;
using Weapons.Concrete.Guns;

namespace Characters
{
    public enum NormalShootingState
    {
        Roam = 0,
        Idle = 1,
        Chase = 2,
        Shooting = 3,
        Die = 4,
    }

    public class NormalShootingEnemy : RangeCheckingEnemy
    {
        public string enemyName;
        public float enterRangeYDistance;
        public float exitRangeYDistance;
        public float exitRangeRadius;
        public float turningSpeed;
        public SlimeWeapon weapon;
        public Animator animator;
        public Collider enemyCollider;
        // public GameObject slimeModel;
        public DamageCanvas damageCanvas;

        private EnemyMovingController _movingControllerScript;
        private NormalShootingState _state;
        private List<GameObject> _chasingTargets;
        private GameObject _chasingTarget;

        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Chase = Animator.StringToHash("chase");
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Attack = Animator.StringToHash("attack");

        // private RaycastHit _hit;
        // private Ray _ray;
        

        // debug
        float m_Theta = 0.1f;

        private void Start()
        {
            _state = NormalShootingState.Idle;
            _movingControllerScript = GetComponent<EnemyMovingController>();
            _chasingTargets = new List<GameObject>();
        }
        protected override void ZeroHpHandle()
        {
            _movingControllerScript.StopMoving();
            animator.SetTrigger(Die);
            Invoke("SelfDestroy", 3f);
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject otherObj = other.gameObject;
            if (otherObj.layer == LayerMask.NameToLayer("pBullet"))
            {
                AbstractBullet bulletScript = otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                var damage = (int) bulletScript.damage;
                Hurt(damage);
                damageCanvas.show(damage);
            }
        }

        private void Update()
        {
            if (_state == NormalShootingState.Chase)
            {
                if (weapon.CanShoot())
                {
                    animator.SetTrigger(Attack);
                    weapon.Shoot(_chasingTarget.transform);
                }
            }
        }

        public override void PlayerEnterInnerRange(Collider player)
        {
            if (_chasingTargets.Count == 4) return;
            if (_state == NormalShootingState.Die) return;
            if (Math.Abs(player.transform.position.y - transform.position.y) > enterRangeYDistance) return;
            
            _chasingTargets.Add(player.gameObject);
            if (_chasingTarget == null)
            {
                _chasingTarget = player.gameObject;
                _movingControllerScript.EnableMoving();
                _state = NormalShootingState.Chase;
                animator.SetTrigger(Chase);
                // _state = NormalShootingState.Shooting;
                // animator.SetTrigger(Attack);
                _movingControllerScript.MoveToTarget(_chasingTarget);
            }
        }

        private void PlayerExit(GameObject obj)
        {
            if (_chasingTargets.Contains(obj)) _chasingTargets.Remove(obj);
            if (obj.GetInstanceID() == _chasingTarget.GetInstanceID())
            {
                if (_chasingTargets.Count > 0)
                {
                    // chase the last entered player
                    _chasingTarget = _chasingTargets[_chasingTargets.Count - 1];
                }
                else
                {
                    // no player in target array, set trigger Idle
                    _chasingTarget = null;
                    _state = NormalShootingState.Idle;
                    _movingControllerScript.StopMoving();
                    animator.SetTrigger(Idle);
                }
            }
        }

        // evoked by animator
        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

    }
}

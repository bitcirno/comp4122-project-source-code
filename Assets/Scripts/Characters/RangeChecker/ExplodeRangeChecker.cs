using System;
using System.Collections.Generic;
using Characters.MovingController;
using TMPro;
using UnityEngine;

namespace Characters.RangeChecker
{
    public class ExplodeRangeChecker : MonoBehaviour
    {
        public float explosionPower;

        private List<int> _effectedObjInstanceIds;

        private void Start()
        {
            _effectedObjInstanceIds = new List<int>();
            Invoke(nameof(SelfDestroy), 1f);   // disappear in 1 sec
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public void SetExplosionPower(float strength)
        {
            explosionPower = strength;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;  // only take effect to players
            
            var instanceId = other.GetInstanceID();
            if (_effectedObjInstanceIds.Contains(instanceId)) return;  // each player can only be add such effect once
            _effectedObjInstanceIds.Add(instanceId);

            var playerScript = other.GetComponent<Player>();
            var playerMovingScript= other.GetComponent<PlayerMovingController>();
            
            // hurt value is a quarter of (21 - distance)^2 
            // the max value of _sphereCollider.radius is 20
            var position = transform.position;
            position.y -= 1f;
            var playerPos = other.transform.position;
            var distance = Vector3.Distance(playerPos, position);
            
            // player hurt
            var hurt = (int) (Math.Pow(21 - distance, 2) * 0.25);
            playerScript.Hurt(hurt);
            // Debug.Log($"player hurt: {hurt}");
            PlayerInfo.Instance.lossHp(hurt);

            var direction = (playerPos - position).normalized;
            var force = Mathf.Clamp(explosionPower * (21 - distance), 0, 1000);
            playerMovingScript.AddImpact(direction, force);
        }
    }
}
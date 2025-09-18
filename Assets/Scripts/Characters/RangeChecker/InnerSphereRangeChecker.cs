using System;
using AbstractClass;
using UnityEngine;

namespace Characters.RangeChecker
{
    public class InnerSphereRangeChecker : MonoBehaviour
    {
        public RangeCheckingEnemy enemyObj;
        
        private void OnTriggerEnter(Collider other)
        {
            enemyObj.PlayerEnterInnerRange(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            enemyObj.PlayerExitInnerRange(other);
        }
    }
}
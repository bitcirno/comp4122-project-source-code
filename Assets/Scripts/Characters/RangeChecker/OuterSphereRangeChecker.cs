using UnityEngine;

namespace Characters.RangeChecker
{
    public class OuterSphereRangeChecker : MonoBehaviour
    {
        public RangeCheckingEnemy enemyObj;
        
        private void OnTriggerEnter(Collider other)
        {
            enemyObj.PlayerEnterOuterRange(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            enemyObj.PlayerExitOuterRange(other);
        }
    }
}
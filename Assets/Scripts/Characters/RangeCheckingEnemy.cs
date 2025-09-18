using AbstractClass;
using UnityEngine;

namespace Characters
{
    public abstract class RangeCheckingEnemy : AbstractHpObject
    {
        // Enemy that has ranges to check whether player is in range

        public virtual void PlayerEnterInnerRange(Collider player) { }
        public virtual void PlayerExitInnerRange(Collider player) { }
        public virtual void PlayerEnterOuterRange(Collider player) { }
        public virtual void PlayerExitOuterRange(Collider player) { }
    }
}

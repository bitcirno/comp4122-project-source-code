using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbstractHpObject : MonoBehaviour
    {
        // attached to objects with the HP property

        [SerializeField] protected int hp;  // current hp
        [SerializeField] protected int hpMax;  // max hp
        private bool _isInvincible = false;  // whether it is invincible

        protected abstract void ZeroHpHandle();  // evoked when hp decreases to 0

        public void SetHp(int initValue)  // init hp values
        {
            hpMax = initValue;
            hp = initValue;
        }

        public void SetCurrentHp(int curHp)
        {
            hp = curHp;
        }

        public int GetHp()
        {
            return hp;
        }

        public void ToInvincible()
        {
            _isInvincible = true;
        }

        public void EndInvincible()
        {
            _isInvincible = false;
        }

        public void ExtendHpMaxByValue(int increasedValue)  // extend max hp
        {
            hpMax += increasedValue;
            PlayerInfo.Instance.init(hp, hpMax);
        }

        public void Hurt(int damageValue)  // hurt: hp decreases
        {
            // if (_isInvincible) return;
            hp = Math.Max(hp - damageValue, 0);
            if (hp == 0) ZeroHpHandle();
        }

        public void Heal(int increasedValue)  // heal: hp increases
        {
            // if (!_isInvincible) return;
            hp = Math.Min(hp + increasedValue, hpMax);
            PlayerInfo.Instance.init(hp);
        }
    }
}

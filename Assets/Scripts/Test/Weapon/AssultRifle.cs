using UnityEngine;

namespace Scripts.Weapon
{
    public class AssualtRifle : Firearms
    {
        protected override void Shooting()
        {
            Debug.Log("Shooting");
        }

        protected override void Reload()
        {

        }
    }
}

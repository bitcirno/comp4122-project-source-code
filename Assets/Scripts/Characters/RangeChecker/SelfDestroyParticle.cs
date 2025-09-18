using UnityEngine;

namespace Characters.RangeChecker
{
    public class SelfDestroyParticle : MonoBehaviour 
    {
        private void Start()
        {
            var main = GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Callback;
            
        }
        
        private void OnParticleSystemStopped()
        {
            Destroy(gameObject);
        }
    }
}
using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.WSA;

namespace UIFramework.Managers
{
    public class PostProcessingManager : MonoBehaviour
    {
        private PostProcessVolume _volume;
        // private List<PostProcessEffectSettings> _settings;
        private ChromaticAberration _chromatic;
        private LensDistortion _lensDistortion;
        private int _isFastRun = 0;

        public float _chromaticChangeSpeed;
        public float _lenDistortionChangeSpeed;

        public static PostProcessingManager Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            _volume = GetComponent<PostProcessVolume>();
            _volume.profile.TryGetSettings(out _chromatic);
            _volume.profile.TryGetSettings(out _lensDistortion);
        }

        public void EndFastRun()
        {
            _isFastRun = -1;
        }

        private void Update()
        {
            if (!GlobalManager.Instance.IsPlayerAlive()) return;
            
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) _isFastRun = 1;
            else _isFastRun = -1;
            
            if (_isFastRun == 1)
            {
                _chromatic.intensity.value += Time.deltaTime * _chromaticChangeSpeed;
                _lensDistortion.intensity.value -= Time.deltaTime * _lenDistortionChangeSpeed;
                if (_lensDistortion.intensity.value < -45)
                {
                    _lensDistortion.intensity.value = -45;
                }
                if (_chromatic.intensity.value >= 1)
                {
                    _chromatic.intensity.value = 1;
                    _isFastRun = 0;
                }
            }
            else if (_isFastRun == -1)
            {
                _chromatic.intensity.value -= Time.deltaTime * _chromaticChangeSpeed;
                _lensDistortion.intensity.value += Time.deltaTime * _lenDistortionChangeSpeed;
                if (_lensDistortion.intensity.value > 0)
                {
                    _lensDistortion.intensity.value = 0;
                }
                if (_chromatic.intensity.value <= 0)
                {
                    _chromatic.intensity.value = 0;
                    _isFastRun = 0;
                }
            }
        }
    }
}
using System.Collections.Generic;
using Characters;
using Cinemachine;
using UIFramework.Managers;
using UnityEngine;

namespace Managers
{
    public class GlobalManager : MonoBehaviour
    {
        // global variable management
        public Player mainPlayer;  // the main player
        private CinemachineVirtualCamera _virtualCamera;
        private Vector3 _savePoint;

        public bool isGamePaused = false;  // is the game paused
        public AstarPath astarPath;

        private bool _isPlayerAlive = true;

        public static GlobalManager Instance { get; private set; }

        // keyboard key binding
        public Dictionary<string, KeyCode> KeyBinding = new Dictionary<string, KeyCode>
        {
            {"player move forward", KeyCode.W},
            {"player move backward", KeyCode.S},
            {"player move left", KeyCode.A},
            {"player move right", KeyCode.D},
            {"player jump", KeyCode.Space},
            {"player dodge", KeyCode.LeftShift},
        };

        public void ScanSceneMap()
        {
            astarPath.Scan();
        }

        public void ResetKeyBinding(string keyName, KeyCode keyCode)
        {
            // reset the keyboard key binding
            KeyBinding[keyName] = keyCode;
        }

        public void SetSavePoint(Vector3 pos)
        {
            _savePoint = pos;
        }
        
        public void Respawn(Transform cameraFollow)
        {
            mainPlayer.gameObject.transform.position = _savePoint;
            Debug.Log(_savePoint);
            _virtualCamera.Follow = cameraFollow;
            // reset ui hp
        }

        private void Awake()
        {
            Instance = this;
        }
        
        public void AssignMainPlayer(Player player)
        {
            mainPlayer = player;
        }

        public void AssignVirtualCamera(CinemachineVirtualCamera virtualCamera)
        {
            _virtualCamera = virtualCamera;
        }

        public void GamePause()
        {
            // pause the game
            isGamePaused = true;
        }

        public void GameResume()
        {
            // resume the game
            isGamePaused = false;
        }

        public bool IsPlayerAlive()
        {
            return _isPlayerAlive;
        }

        public void PlayerRetireWithNoHp()
        {
            // _isPlayerAlive = false;
            PostProcessingManager.Instance.EndFastRun();
        }
        
        public void PlayerRetireFalling()
        {
            _isPlayerAlive = false;
            _virtualCamera.Follow = null;
            PostProcessingManager.Instance.EndFastRun();

        }
    }
}

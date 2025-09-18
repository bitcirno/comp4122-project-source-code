using AbstractClass;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Characters
{
    public class Player : AbstractHpObject
    {
        public string playerName;
        public float playerFallYPos;

        public GameObject weaponHolder;

        private void Start()
        {
            PlayerInfo.Instance.init(hp, hpMax);
            // Debug.Log($"player hp: {hp}");
            GlobalManager.Instance.AssignMainPlayer(this);
            GlobalManager.Instance.SetSavePoint(transform.position);
        }
        private void Update()
        {
            if (transform.position.y < -50)
            {
                Debug.Log("DIE");
                SaveLoadManager.Instance.clearSaveData();
                GameRoot.Instance.SceneSystem.SetScene(new EndScene("YOU DIE"));
            }

            if (GlobalManager.Instance.IsPlayerAlive())
            {
                if (transform.position.y < playerFallYPos)
                {
                    GlobalManager.Instance.PlayerRetireFalling();
                }
            }
        }

        protected override void ZeroHpHandle()
        {
            Debug.Log(" ===================== Player retires with 0 hp =====================");
            SaveLoadManager.Instance.clearSaveData();
            GameRoot.Instance.SceneSystem.SetScene(new EndScene("YOU DIE"));
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject otherObj = other.gameObject;
            if (otherObj.layer == LayerMask.NameToLayer("eBullet"))
            {
                // Debug.Log("hit");
                var bulletScript = otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                int dmg = (int)bulletScript.damage;
                Hurt(dmg);
                PlayerInfo.Instance.lossHp(dmg);
            }
        }

        public void ThirdPerson()
        {
            CinemachineVirtualCamera cinemachineVirtualCamera =
                GameObject.Find("CM").gameObject.GetComponent(typeof(CinemachineVirtualCamera)) as
                    CinemachineVirtualCamera;
            cinemachineVirtualCamera.Follow = transform.Find("aim3");
        }

        public void FirstPerson()
        {
            CinemachineVirtualCamera cinemachineVirtualCamera =
                GameObject.Find("CM").gameObject.GetComponent(typeof(CinemachineVirtualCamera)) as
                    CinemachineVirtualCamera;
            cinemachineVirtualCamera.Follow = transform.Find("aim1");
        }
    }
}

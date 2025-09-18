using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Managers
{
    /// <summary>
    /// initialize: create a Sprite directory, enum for mapping
    /// assign the gun type with different sprite (awake)
    /// check the ray cast of the middle of the camera, if hit, change color
    /// further design on the sprite could be modify in the if-else statement.
    /// </summary>
    public enum CrossHair // CrossHair.Pistol(CrossHair.Name) = 0 
    {
        Pistol = 0,
        Rifle = 1,
        Shotgun = 2,
    }

    public class AimCrossHairManager : MonoBehaviour
    {
        // a class to manage the cross hair UI
        // the Cross shapes in Sprite
        public Sprite pistolCrossHair;
        public Sprite rifleCrossHair;
        public Sprite shotgunCrossHair;

        [Space] // show a space in the inspector
        public Color crossHaiOriginalColor; // the normal color without targeting
        public Color targetingEnemyColor; // the color when move on enemy 
        public Transform virtualCamera; // not used

        [Space] public bool detectingEnemyAiming = true;
        public float enemyDetectRaycastDistance = 10f;

        private Dictionary<CrossHair, Sprite> crossHairSprites; // directory: a data struc to hold the Sprite
        private Image _crossHairImage;  // the image component
        public Vector3 targetPoint;
        private RaycastHit hitInfo;


        public static AimCrossHairManager Instance { get; private set; }

        // private CrossHair _crossHair = CrossHair.Pistol;  // the current cross hair used

        private void Awake()
        {
            Instance = this;
            crossHairSprites = new Dictionary<CrossHair, Sprite> // initialize variety of Sprites
            {
                {CrossHair.Pistol, pistolCrossHair},
                {CrossHair.Rifle, rifleCrossHair},
                {CrossHair.Shotgun, shotgunCrossHair},
            };
            _crossHairImage = GetComponent<Image>(); // get the image component

            ChangeCrossHair(CrossHair.Pistol);  // assign different image to _crossHairImage
        }

        private void Update()
        {
            if (detectingEnemyAiming)
            {
                var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hitInfo, Int32.MaxValue, layerMask:1<<6|1<<7|1<<9|1<<11))
                {
                    targetPoint = hitInfo.point;  //记录碰撞的目标点
                    if (hitInfo.collider.gameObject.layer == 9) TargetEnemy();
                    else LoseEnemyTargeting();
                }
                else targetPoint = ray.direction * 500;
            }
        }

        public void ChangeCrossHair(CrossHair newCrossHair)
        {
            // change the cross hair
            _crossHairImage.sprite = crossHairSprites[newCrossHair];
        }

        private void TargetEnemy()
        {
            // cross hair turns red when aiming at enemies
            _crossHairImage.color = targetingEnemyColor;
        }
        private void LoseEnemyTargeting()
        {
            // cross hair turns white losing targeting enemies
            _crossHairImage.color = crossHaiOriginalColor;
        }
    }
}

using System;
using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        private void Start()
        {
            // hide cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}

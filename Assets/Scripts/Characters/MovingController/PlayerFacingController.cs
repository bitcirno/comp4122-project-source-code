using Cinemachine;
using Managers;
using UnityEngine;

namespace Characters.MovingController
{
    public class PlayerFacingController : MonoBehaviour
    {
        /// <summary>
        /// This program changes the camera focusing.
        /// the camera follows the aim and looks at aim point
        /// Rotation around Y axis, rotate the player
        /// rotation around the X axis, rotate the aim position
        /// use lerp to change the field of view 
        /// </summary>
        /// 
        // player orientation
        // private Vector3 _facingVector;
        // private Vector3 _facingOriVector = Vector3.forward;
        private bool _playerRotatable = true;  // is the player rotatable
        private float _playerRotateSpeed;  // player rotation angular speed
        public float playerRotateSpeedOri;  // player rotation angular speed commonly (1.5)
        public float playerRotateSpeedLow;  // player rotation angular speed when scaling view filed (0.5)
        private GameObject _cameraHome;

        public GameObject aimObject;  // aim object
        public CinemachineVirtualCamera virtualCamera;  // virtual camera script

        private readonly float[] _rotateRangeX = { -83f, 33f }; // once set it can't be changed
        private float _angleX;

        private readonly float[] _viewFieldRange = { 30f, 60f };  // change the field of view
        public float viewFieldChangeSpeed;
        private bool _viewFieldScaling;  // is changing the field of view
        private float dt = 1; //24 * Time.deltaTime; // this is the attempt to eliminate the quivering of camera 
        private void Start()
        {
            _playerRotateSpeed = playerRotateSpeedOri;
            GlobalManager.Instance.AssignVirtualCamera(virtualCamera);
            _cameraHome = GameObject.Find("Cameras");
            virtualCamera.transform.parent = _cameraHome.transform;
        }

        private void Update()
        {
            if (!GlobalManager.Instance.IsPlayerAlive()) return;  // player can operate when not retired
            
            if (_playerRotatable)
            {
                ChangeYRotationWithValue(Input.GetAxis("Mouse X") * _playerRotateSpeed * dt); // rotate around Y axis
                _angleX += Input.GetAxis("Mouse Y") * -_playerRotateSpeed * dt; // calculate the Y (vertical) with limitation
                if (_angleX < _rotateRangeX[0]) _angleX = _rotateRangeX[0];
                else if (_angleX > _rotateRangeX[1]) _angleX = _rotateRangeX[1];
                ChangeXRotationToValue(_angleX); // change the rotate
            }

            // filed of view scale
            if (Input.GetMouseButtonDown(1))
            {
                _viewFieldScaling = true;
                _playerRotateSpeed = playerRotateSpeedLow;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _viewFieldScaling = false;
                _playerRotateSpeed = playerRotateSpeedOri;
            }

            if (_viewFieldScaling)
            {
                // decrease field of view
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
                    _viewFieldRange[0], Time.deltaTime * viewFieldChangeSpeed);
            }
            else if (virtualCamera.m_Lens.FieldOfView < 59)
            {
                // field if view resumes
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
                    _viewFieldRange[1], Time.deltaTime * viewFieldChangeSpeed);
            }

        }

        private void ChangeYRotationWithValue(float value)
        {
            // aim view turning horizontally
            var transform1 = transform; // the shaking problem could also caused by the rotation center problem
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.y += value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }

        private void ChangeXRotationToValue(float value)
        {
            // aim view turning vertically
            var transform1 = aimObject.transform;
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.x = value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }
    }
}

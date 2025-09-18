using System;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbstractMovingObject : MonoBehaviour
    {
        // can be attached on game objects singly,
        // with moving and jumping functions packaged

        private float _moveSpeed;  // move speed
        private float _moveSpeedBackup;  // move speed backup (used for resetting)
        private bool _isMoveable;  // can move flag

        private float _jumpSpeed;  // jump speed (vertical)
        private float _jumpSpeedBackup;  //  jump speed backup

        protected void InitMoveSpeed(float speed)
        {
            // init move speed
            _moveSpeed = speed;
            _moveSpeedBackup = speed;
        }

        protected void InitJumpSpeed(float speed)
        {
            // init jump speed
            _jumpSpeed = speed;
            _jumpSpeedBackup = speed;
        }

        public void ImmediateFreezeMoving()
        {
            _isMoveable = false;
        }

        public void UnfreezeMoving()
        {
            _isMoveable = true;
        }

        protected float GetMoveSpeed()
        {
            // get move speed
            return _moveSpeed;
        }

        protected float GetJumpSpeed()
        {
            // get jump speed
            return _jumpSpeed;
        }


        public void ResetMoveSpeed()
        {
            // reset the move speed
            _moveSpeed = _moveSpeedBackup;
        }

        public void ResetJumpSpeed()
        {
            // reset the jump speed
            _jumpSpeed = _jumpSpeedBackup;
        }

        public void ForceSetMoveSpeed(float speed)
        {
            // reset the move speed by force (with a given value that might exceed the max value)
            _moveSpeed = speed;
        }

        public void ForceSetJumpSpeed(float speed)
        {
            // reset the jump speed by force (with a given value that might exceed the max value)
            _jumpSpeed = speed;
        }

        public abstract bool CanMove();  // judge whether it meets the requirements of moving
    }
}

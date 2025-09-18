using System;
using AbstractClass;
using Pathfinding;
using UnityEngine;
using UnityEngine.WSA;

namespace Characters.MovingController
{
    public class EnemyMovingController : AbstractMovingObject
    {
        // A* path finding management
        public float moveSpeed;
        public float turningSpeed;  // the speed that the AI turns
        private GameObject _target;

        public float nextWayPointDistance;  // the distance os selecting the next way point
        private Path _path;  // the found path will be stored in it
        private int _currentWayPoint = 0;  // the indexof the surrent
        private Vector3 _wayPoint;
        private int _maxWayPoint;
        private CharacterController _characterController;

        private Seeker _seeker;
        private Rigidbody _rgBody;

        private bool _startMoving;
        private bool _enableMoving;
        private bool _isFaceTurning;

        public float calculateNextPathInterval = 3;  // （安路径行走时）重新计算下一次路径的时间间隔
        private float _nextCalculatePathTime;  // 重新计算下一次路径的时间

        protected void Start()
        {
            InitMoveSpeed(moveSpeed);
            _seeker = GetComponent<Seeker>();
            _rgBody = GetComponent<Rigidbody>();
            // _characterController = GetComponent<CharacterController>();
        }

        public void MoveToTarget(GameObject target)
        {
            if (!_enableMoving) return;
            _target = target;
            _seeker.StartPath(transform.position, target.transform.position, OnPathComplete);  // start an A* path finding
        }

        public void EnableMoving()
        {
            _enableMoving = true;
        }

        private void OnPathComplete(Path path)
        {
            // the callback function of _seeker.StartPath()
            if (!_enableMoving) return;
            if (path.error) return;
            _path = path;
            _currentWayPoint = 0;
            _startMoving = true;
            _maxWayPoint = _path.vectorPath.Count - 1;  // minus one减一，是两次寻路无缝衔接
            _nextCalculatePathTime = Time.time + calculateNextPathInterval;  // 计算重新计算一次路径的时间
            _wayPoint = _path.vectorPath[_currentWayPoint];
            _wayPoint.y = transform.position.y;
        }

        private void Update()
        {
            if (!_startMoving) return;

            // 时间到，重新计算一次路径
            if (Time.time > _nextCalculatePathTime)
            {
                MoveToTarget(_target);
                _nextCalculatePathTime = Time.time + calculateNextPathInterval;
            }

            if (Vector2.Distance(transform.position, _wayPoint) < nextWayPointDistance)
            {
                // 移动到离下个路径点很近（由nextWayPointDistance衡量）的位置
                _currentWayPoint++;
                _wayPoint = _path.vectorPath[_currentWayPoint];
                _isFaceTurning = true;
                _wayPoint.y = transform.position.y;
                if (_currentWayPoint >= _maxWayPoint)
                {
                    // reach the second last way point of the path, start new path finding
                    MoveToTarget(_target);
                    _nextCalculatePathTime = Time.time + calculateNextPathInterval;
                }
            }

            if (_isFaceTurning)
            {
                //平滑旋转看向某一个点
                Quaternion quaternion = Quaternion.LookRotation(_wayPoint - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, turningSpeed * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (!_startMoving) return;
            // // 算出方向的单位向量
            var direction = (_wayPoint - transform.position).normalized;
            var force = direction * GetMoveSpeed() * Time.deltaTime;
            // _characterController.Move(force);
            _rgBody.AddForce(force);
        }

        public override bool CanMove()
        {
            return true;
        }

        public void StopMoving()
        {
            _enableMoving = false;
            _startMoving = false;
        }

    }
}
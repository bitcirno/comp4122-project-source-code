using System.Collections;
using Managers;
using UnityEngine;

namespace Characters.MovingController
{
    public class PlayerMovingController : MonoBehaviour
    {
        private CharacterController characterController;
        private Animator characterAnimator;
        private Vector3 movementDirection;
        private Transform characterTransform;
        private float velocity;
        private Vector3 _impact = Vector3.zero;

        private Animator animator;
        private static AudioSource Sound;

        private bool isCrouched;
        private float originHeight;

        public float SprintingSpeed;
        public float WalkSpeed;

        public float SprintingSpeedWhenCrouched;
        public float WalkSpeedWhenCrouched;

        public float Gravity = 9.8f;
        public float JumpHeight;
        public float CrouchHeight = 1f;

        public float mass;
        public AudioClip[] WalkSound;
        public AudioClip[] JumpSound;
        private void Start()
        {
            Sound = gameObject.GetComponent<AudioSource>();
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
            characterTransform = transform;
            originHeight = characterController.height;
            animator = gameObject.GetComponent<Animator>();
            Sound = GetComponent<AudioSource>();
            Sound.volume = 1f;
        }

        private void Update()
        {
            UpdateImpact();
            if (!GlobalManager.Instance.IsPlayerAlive())
            {
                var tmp_Velocity = characterController.velocity;
                tmp_Velocity.y = 0;
                velocity = tmp_Velocity.magnitude;
                movementDirection.y -= Gravity * Time.deltaTime;
                characterController.Move(WalkSpeed * Time.deltaTime * movementDirection);
                return;
            }

            float tmp_CurrentSpeed = WalkSpeed;
            if (characterController.isGrounded)
            {
                var tmp_Horizontal = Input.GetAxis("Horizontal");
                var tmp_Vertical = Input.GetAxis("Vertical");
                movementDirection =
                    characterTransform.TransformDirection(new Vector3(tmp_Horizontal, 0, tmp_Vertical));

                animator.SetBool("isMovingF", tmp_Vertical > 0);
                animator.SetBool("isMovingB", tmp_Vertical < 0);
                animator.SetBool("isMovingR", tmp_Horizontal > 0);
                animator.SetBool("isMovingL", tmp_Horizontal < 0);
                animator.SetBool("isMoving", tmp_Horizontal != 0 || tmp_Vertical != 0);
                if (!Sound.isPlaying && (tmp_Horizontal != 0 || tmp_Vertical != 0))
                {
                    Sound.clip = WalkSound[Random.Range(0, WalkSound.Length)];
                    Sound.Play();
                }


                if (Input.GetButtonDown("Jump"))
                {
                    Sound.clip = JumpSound[Random.Range(0, JumpSound.Length)];
                    Sound.volume = 0.6f;
                    Sound.Play();
                    movementDirection.y = JumpHeight;
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    var tmp_CurrentHeight = isCrouched ? originHeight : CrouchHeight;
                    StartCoroutine(DoCrouch(tmp_CurrentHeight));
                    isCrouched = !isCrouched;
                }

                if (isCrouched)
                {
                    tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeedWhenCrouched : WalkSpeedWhenCrouched;
                }
                else
                {
                    tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeed : WalkSpeed;
                }

                var tmp_Velocity = characterController.velocity;
                tmp_Velocity.y = 0;
                velocity = tmp_Velocity.magnitude;
                // characterAnimator.SetFloat("Velocity", velocity, 0.25f, Time.deltaTime);
            }

            movementDirection.y -= Gravity * Time.deltaTime;
            characterController.Move(tmp_CurrentSpeed * Time.deltaTime * movementDirection);
        }

        private IEnumerator DoCrouch(float _target)
        {
            float tmp_CurrentHeight = 0;
            while (Mathf.Abs(characterController.height - _target) > 0.1f)
            {
                yield return null;
                characterController.height =
                    Mathf.SmoothDamp(characterController.height, _target,
                        ref tmp_CurrentHeight, Time.deltaTime * 5);
            }
        }

        // added
        public void AddImpact(Vector3 dir, float force)
        {
            dir.Normalize();
            if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
            _impact += dir.normalized * force / mass;
        }

        public static float getVolume()
        {
            return Sound.volume;
        }

        public static void changeVolume(float value)
        {
            Sound.volume = value;
        }

        private void UpdateImpact()
        {
            // apply the impact force:
            if (_impact.magnitude > 0.2) characterController.Move(_impact * Time.deltaTime);
            // consumes the impact energy each cycle:
            _impact = Vector3.Lerp(_impact, Vector3.zero, 5 * Time.deltaTime);
        }

    }
}

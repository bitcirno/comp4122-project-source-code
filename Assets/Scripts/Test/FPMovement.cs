using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FPMovement : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public float Gravity;

    private Transform characterTransform;
    private Rigidbody characterRigidbody;
    private bool isGrounded;

    void Start()
    {
        characterTransform = transform;
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");

            var tmp_CurrentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            tmp_CurrentDirection = characterTransform.TransformDirection(tmp_CurrentDirection);
            tmp_CurrentDirection *= Speed;

            var tmp_CurrentVelocity = characterRigidbody.velocity;
            var tmp_VelocityChange = tmp_CurrentDirection - tmp_CurrentVelocity;
            tmp_VelocityChange.y = 0;

            characterRigidbody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);

            if (Input.GetButtonDown("Jump"))
            {
                characterRigidbody.velocity = new Vector3(tmp_CurrentVelocity.x, CalculateJumpHeightSpeed(),
                    tmp_CurrentVelocity.z);
            }
        }

        characterRigidbody.AddForce(new Vector3(0, -Gravity * characterRigidbody.mass, 0));
    }

    private float CalculateJumpHeightSpeed()
    {
        return Mathf.Sqrt(2 * Gravity * JumpHeight);
    }


    private void OnCollisionStay(Collision _other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision _other)
    {
        isGrounded = false;
    }
}

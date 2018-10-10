﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTwo : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 20;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private float horizontalInput;
    private bool isOnGround;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

    void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        Debug.Log("IsOnGround?: " + isOnGround);
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate ()
    {
        Move();
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
    }
}

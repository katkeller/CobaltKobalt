using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTwo : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 20;

    [SerializeField]
    private float jumpForce = 10;

    private float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
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

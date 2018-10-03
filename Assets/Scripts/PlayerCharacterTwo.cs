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

    private float horizontalInput;
	
	void Start ()
    {
		
	}

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate ()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
	}
}

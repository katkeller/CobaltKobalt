using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    //[SerializeField]
    //private Collider2D pickupDetectTrigger;

    //[SerializeField]
    //private ContactFilter2D pickupContactFilter;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 20;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private float doubleJumpForce = 15;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private float horizontalInput;
    private bool isOnGround;
    private bool canDoubleJump;
    private Collider2D[] groundHitDetectionResults = new Collider2D[26];
    //private Collider2D[] pickupHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private bool facingRight = true;
    private Animator playerAnimator;
    private AudioSource audioSource;
    private bool audioIsPlaying;
    private bool isDead = false;
    private bool pickUpIsActivated = false;
    //private bool isMoving;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateIsOnGround();
        //UpdateCanDoubleJump();
        UpdateHorizontalInput();
        HandleJumpInput();
        UpdateSound();
        CheckForRespawn();
    }

    private void UpdateSound()
    {
        if (isOnGround && !audioIsPlaying && Mathf.Abs(horizontalInput) > 0)
        {
            audioSource.Play();
            audioIsPlaying = true;
        }
        else if (audioIsPlaying && Mathf.Abs(horizontalInput) <= 0 || !isOnGround)
        {
            audioSource.Pause();
            audioIsPlaying = false;
        }
    }

    void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();

        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        //do
        //{
        //    audioSource.Play();
        //} while (Mathf.Abs(horizontalInput) > 0);
    }
    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        Debug.Log("IsOnGround?: " + isOnGround);
    }

    //private void UpdateCanDoubleJump()
    //{
    //    canDoubleJump = pickupDetectTrigger.OverlapCollider(pickupContactFilter, pickupHitDetectionResults) > 0;
    //    Debug.Log("CanDoubleJump?: " + canDoubleJump);
    //}

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (Input.GetButtonDown("DoubleJump") && canDoubleJump && !pickUpIsActivated)
        {
            rb2d.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            pickUpIsActivated = true;
        }
    }
    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            canDoubleJump = true;
            Debug.Log("CanDoubleJump?: " + canDoubleJump);
        }

        //if (other.gameObject.CompareTag("Hazard"))
        //{
        //    isDead = true;
        //}
    }
    public void Death()
    {
        isDead = true;
        playerAnimator.SetBool("isDead", isDead);
    }

    private void CheckForRespawn()
    {
        if (isDead && Input.GetButtonDown("Respawn"))
        {
            Respawn();
        }
    }


    public void Respawn()
    {
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }

        isDead = false;
        playerAnimator.SetBool("isDead", isDead);
    }
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }
        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}


﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerCharacter : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    [Tooltip("The collider that dictates where the player comes into contact with the ground")]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    [Tooltip("The trigger collider that overlaps with the ground")]
    private Collider2D groundDetectTrigger;

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

    [SerializeField]
    private Image respawnUIImage;
    #endregion


    public static event Action PlayerRespawned;

    private float horizontalInput;
    private bool isOnGround;
    private bool canDoubleJump;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private bool facingRight = true;
    private Animator playerAnimator;
    private AudioSource audioSource;
    private bool audioIsPlaying;
    private bool isDead = false;
    private bool pickUpIsActivated = false;
    private bool canMove = true;
    private bool isJumping = false;

    public bool PlayerIsRespawning { get; set; }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        CheckForRespawn();
    }
    /// <summary>UpdateSound either plays or stops the typing/footstep sound</summary>
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
        UpdateSound();
        UpdatePlayerAnimator();

    }

    public void Respawn()
    {
        PlayerIsRespawning = true;
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
        respawnUIImage.enabled = false;
        canMove = true;
        if (PlayerRespawned != null)
            PlayerRespawned.Invoke();
    }

    private void UpdatePlayerAnimator()
    {
        if (isOnGround)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }
    }

    /// <summary>This method keeps the player from sliding around or being too slow to speed up due to physics materials</summary>
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
        
        if (isOnGround )
        {
            playerAnimator.SetBool("isJumping", false);
        }
        else if (!isOnGround && !isDead)
        {
            playerAnimator.SetBool("isJumping", true);
        }
    }

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
            StartCoroutine(DelayPickUpReset());
        }
    }

    IEnumerator DelayPickUpReset()
    {
        yield return new WaitForSeconds(3.0f);
        pickUpIsActivated = false;
    }

    private void Move()
    {
        if (canMove)
        {
            rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2d.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = clampedVelocity;
        }

        UpdateDirectionFacing();
    }

    private void UpdateDirectionFacing()
    {
        if (horizontalInput > 0 && !facingRight && canMove)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight && canMove)
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            canDoubleJump = false;
        }
    }

    public void Die()
    {
        canMove = false;
        rb2d.velocity = Vector2.zero;
        isDead = true;
        playerAnimator.SetBool("isDead", isDead);
        respawnUIImage.enabled = true;
    }

    private void CheckForRespawn()
    {
        if (isDead && Input.GetButtonDown("Respawn"))
        {
            Respawn();
        }
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


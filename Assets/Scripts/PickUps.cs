using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator pickUpAnimator;
    //private bool playerIsRespawning;
    private PlayerCharacter player;
    private bool PlayerInTrigger;
    private bool pickUpIsActivated;
    public bool playerIsRespawning;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pickUpAnimator = GetComponent<Animator>();
    }
    void Update ()
    {
        if (PlayerInTrigger && Input.GetButtonDown("DoubleJump") && !pickUpIsActivated)
        {
            audioSource.Play();
            pickUpIsActivated = true;
        }
        
        if (pickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", true);
        }
        else if (!pickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", false);
        }

        if (playerIsRespawning)
        {
            pickUpIsActivated = false;
        }
        
	}

    public void PlayerIsRespawing()
    {
        playerIsRespawning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            PlayerInTrigger = true;
            pickUpAnimator.SetBool("playerInTrigger", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            PlayerInTrigger = false;
            pickUpAnimator.SetBool("playerInTrigger", false);
        }
    }
}

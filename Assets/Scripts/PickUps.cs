using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator pickUpAnimator;
    private bool PlayerInTrigger;
    private bool pickUpIsActivated;

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
            StartCoroutine(DelayPickUpReset());
            PlayerInTrigger = false;
        }

        UpdateAnimation();
	}

    private void UpdateAnimation()
    {
        if (pickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", true);
        }
        else if (!pickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", false);
        }

        if (PlayerInTrigger)
        {
            pickUpAnimator.SetBool("playerInTrigger", true);
        }
        else if (!PlayerInTrigger)
        {
            pickUpAnimator.SetBool("playerInTrigger", false);
        }
    }

    IEnumerator DelayPickUpReset()
    {
        yield return new WaitForSeconds(3.0f);
        pickUpIsActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickUpIsActivated)
        {
            PlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickUpIsActivated)
        {
            PlayerInTrigger = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator pickUpAnimator;
    //private bool isActive = false;
    private bool canDoubleJump;
    private bool pickUpIsActivated = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pickUpAnimator = GetComponent<Animator>();
    }
    void Update ()
    {
        if (canDoubleJump && Input.GetButtonDown("DoubleJump"))
        {
            audioSource.Play();
            pickUpAnimator.SetBool("isActivated", true);
            pickUpIsActivated = true;
            canDoubleJump = false;
        }
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            canDoubleJump = true;
            pickUpAnimator.SetBool("playerInTrigger", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            canDoubleJump = false;
            pickUpAnimator.SetBool("playerInTrigger", false);
        }
    }

}

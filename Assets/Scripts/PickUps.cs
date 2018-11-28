using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator pickUpAnimator;
    private bool playerIsRespawning;
    // private bool canDoubleJump;

    public bool PlayerInTrigger { get; set; }
    public bool PickUpIsActivated { get; set; }

    //public bool PickUpIsActivated
    //{
    //    get { return pickUpIsActivated; }
    //    set { pickUpIsActivated = value; }
    //}

    //private bool pickUpIsActivated = false;

    private void Start()
    {
        PlayerCharacter player = new PlayerCharacter();
        playerIsRespawning = player.PlayerIsRespawning;
        audioSource = GetComponent<AudioSource>();
        pickUpAnimator = GetComponent<Animator>();
    }
    void Update ()
    {
        if (PlayerInTrigger && Input.GetButtonDown("DoubleJump") && !PickUpIsActivated)
        {
            audioSource.Play();
            //pickUpAnimator.SetBool("isActivated", true);
            PickUpIsActivated = true;
        }
        
        if (PickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", true);
        }
        else if (!PickUpIsActivated)
        {
            pickUpAnimator.SetBool("isActivated", false);
        }

        if (playerIsRespawning)
        {
            Debug.Log("pickup is being reset");
        }
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PickUpIsActivated)
        {
            PlayerInTrigger = true;
            pickUpAnimator.SetBool("playerInTrigger", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PickUpIsActivated)
        {
            PlayerInTrigger = false;
            pickUpAnimator.SetBool("playerInTrigger", false);
        }
    }

}

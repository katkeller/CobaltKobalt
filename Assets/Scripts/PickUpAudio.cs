using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private bool pickUpIsActivated = false;
    private bool audioIsPlaying = false;
    private bool playerInTrigger = false;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
		if (playerInTrigger && Input.GetButtonDown("DoubleJump"))
        {
            pickUpIsActivated = true;
            audioSource.Stop();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated && !audioIsPlaying)
        {
            playerInTrigger = true;
            audioSource.Play();
            audioIsPlaying = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated && audioIsPlaying)
        {
            playerInTrigger = false;
            audioSource.Stop();
            audioIsPlaying = false;
        }
    }
}

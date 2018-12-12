using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to a child object on the pick up. It allows the pick up to play two seperate audio clips simultaneously.
/// </summary>
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
            StartCoroutine(DelayPickUpRest());
        }
	}

    IEnumerator DelayPickUpRest()
    {
        yield return new WaitForSeconds(3.0f);
        pickUpIsActivated = false;
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

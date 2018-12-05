using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 3.0f;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    private Animator fallingPlatformAnimator;
    private bool isTriggered = false;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        fallingPlatformAnimator = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            Debug.Log("Player activated falling tile!");
            audioSource.Play();
            fallingPlatformAnimator.SetTrigger("PlayerActivatedFallingPlatform");
        }
    }

    private void ActivateFall()
    {
        rb2d.isKinematic = false;
    }
}

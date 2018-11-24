using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    //[SerializeField]
    //private Color inactivatedColor, activatedColor;


    //private SpriteRenderer spriteRenderer;
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
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            canDoubleJump = true;
        }
    }

}

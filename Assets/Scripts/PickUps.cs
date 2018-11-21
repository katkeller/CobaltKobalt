using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    //[SerializeField]
    //private Color inactivatedColor, activatedColor;


    //private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    //private bool isActive = false;
    private bool pickUpIsActivated = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Update ()
    {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickUpIsActivated)
        {
            audioSource.Play();
            //spriteRenderer.color = activatedColor;
        }

        //do
        //{
        //    spriteRenderer.sprite = activatedSprite;
        //} while (collision.CompareTag("Player"));
    }

}

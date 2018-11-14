using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField]
    private Color inactivatedColor, activatedColor;


    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isActive = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Update ()
    {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
            spriteRenderer.color = activatedColor;
            //isActive = !isActive;
        }

        //do
        //{
        //    spriteRenderer.sprite = activatedSprite;
        //} while (collision.CompareTag("Player"));
    }

}

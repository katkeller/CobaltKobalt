using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField]
    private float countDown = 3.0f;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            countDown -= Time.deltaTime;
            audioSource.Play();
            if(countDown <= 0.0f)
            {
                rb2d.isKinematic = false;
                Debug.Log("Platform is falling!");
            }
            //Fall();
            Debug.Log("Player activated falling tile!");
            //rb2d.isKinematic = false;
        }
    }
    //private void Fall()
    //{
    //    countDown -= Time.deltaTime;

    //    if (countDown <= 0.0f)
    //    {
    //        Debug.Log("Tile is falling!");
    //        rb2d.isKinematic = false;
    //    }
    //}
}

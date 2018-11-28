using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private Rigidbody2D rb2d;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player activated falling tile!");
            rb2d.isKinematic = false;
        }
    }
}

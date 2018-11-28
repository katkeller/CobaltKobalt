using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 3.0f;

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
            Debug.Log("Player activated falling tile!");
            audioSource.Play();
            StartCoroutine(DelayFall());
        }
    }

    private IEnumerator DelayFall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
    }
}

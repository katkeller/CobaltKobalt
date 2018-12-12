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
    private Vector3 originalPosition;

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        fallingPlatformAnimator = GetComponent<Animator>();
        originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            audioSource.Play();
            fallingPlatformAnimator.SetTrigger("PlayerActivatedFallingPlatform");
            StartCoroutine(DelayFall());
        }
    }

    IEnumerator DelayFall()
    {
        yield return new WaitForSeconds(fallDelay);
        ActivateFall();
    }

    private void ActivateFall()
    {
        rb2d.isKinematic = false;
    }

    public void ResetPlatforms()
    {
        Debug.Log("Platfroms should reset");
        gameObject.transform.position = originalPosition;
        rb2d.velocity = Vector2.zero;
        rb2d.isKinematic = true;
        isTriggered = false;
    }

    private void OnEnable()
    {
        PlayerCharacter.PlayerRespawned += ResetPlatforms;
    }
    private void OnDisable()
    {
        PlayerCharacter.PlayerRespawned -= ResetPlatforms;

    }
}

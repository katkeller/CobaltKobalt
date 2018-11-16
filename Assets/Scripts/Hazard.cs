using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private float respawnDelay = 5.0f;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered Hazard");
            audioSource.Play();
            StartCoroutine(DelayRespawn());
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Respawn();
        }
        else
        {
            Debug.Log("Something other than the player entered hazard");
        }
        
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
    }
}

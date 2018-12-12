﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    [SerializeField]
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
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Death();
        }
        else
        {
            Debug.Log("Something other than the player entered hazard");
        }
    }
}

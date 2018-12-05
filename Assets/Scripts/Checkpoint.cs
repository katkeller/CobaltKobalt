using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   
    private bool isActivated;

    public void SetIsActivated(bool value)
    {
        isActivated = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            Debug.Log("Player entered checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
        }
    }
}

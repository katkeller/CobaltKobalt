using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTwo : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    private float horizontalInput;
	
	void Start ()
    {
		
	}
	
	void Update ()
    {
        horizontalInput = Input.GetAxis("Horizontal");

	}
}

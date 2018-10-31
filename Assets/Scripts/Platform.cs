using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //check to see if the player is there/has left
    //turn on simulation of rb2d

    private CircleCollider2D circleCollider2D;
	void Start ()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
	}
	
	void Update ()
    {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] //to expose a private int, only effects the one right under
    private int playerLives = 3;

    [SerializeField]
    private string playerName = "Steve";

	// Use this for initialization
	void Start ()
    {
        Debug.Log("yo."); //how you print to the console in unity
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

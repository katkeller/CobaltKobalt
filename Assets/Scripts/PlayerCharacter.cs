using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] //to expose a private value, only effects the one right under, or multiple seperated by comma on same line.
    private int playerLives = 3;

    [SerializeField]
    private string playerName = "Steve";


    private float jumpHeight = 1, speed = 5;

    private bool hasKey; //bools must be named as yes/no questions, i.e. "hasKey" not "key."
    private bool isOnGround;
    

	// Use this for initialization
	void Start ()
    {
        string pizza = "yum";
        Debug.Log(pizza); //how you print to the console in unity

	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(0, -.01f, 0); //don't use, does not use physics. RigedBody does.
        
    }
}

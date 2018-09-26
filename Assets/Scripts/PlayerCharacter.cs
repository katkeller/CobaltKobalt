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

    private Rigidbody2D myRigidBody2D;
    

	// Use this for initialization
	void Start ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); //Searches for datatype in game and puts it in the variable so it's not a null reference. Variable must be initialized.
        /*string pizza = "yum";
        Debug.Log(pizza); //how you print to the console in unity*/

        myRigidBody2D.gravityScale = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) //GetKeyDown is just for the first time you hit it, GetKey lets you hold the key down
        {
            MoveLeft();
        }
        
        //transform.Translate(0, -.01f, 0); //don't use, does not use physics. RigedBody does.
    }

    private void MoveLeft()
    {
        myRigidBody2D.velocity = new Vector2(-5, 0);
    }
}

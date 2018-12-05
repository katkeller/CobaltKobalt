using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "SampleScene";

    [SerializeField]
    private float RotationSpeed = 100;

    private bool isPlayerInTrigger;

    private bool isActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void Update()
    {
        UpdateRotation();
        if (Input.GetButtonDown("Activate") && isPlayerInTrigger)
        {
            Debug.Log("Player activated door!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    private void UpdateRotation()
    {
        float rotationSpeed = RotationSpeed;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}

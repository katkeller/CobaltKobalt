using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactivatedRotationSpeed = 100, activatedRotationSpeed = 300;

    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;

    private bool isActivated;

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActivated)
        {
            rotationSpeed = activatedRotationSpeed;
        }
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    private void UpdateScale()
    {
        float scale = inactivatedScale;

        if (isActivated)
        {
            scale = activatedScale;
        }
        transform.localScale = Vector3.one * scale;
    }

    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensetiviti = 100f;
    private float xRotation = 0f;

    public Transform targetPlayer;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensetiviti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensetiviti * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 45);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        targetPlayer.Rotate(Vector3.up * mouseX);
    }
}

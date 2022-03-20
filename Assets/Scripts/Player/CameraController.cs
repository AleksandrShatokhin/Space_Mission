using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensetivity;
    private float xRotation = 0f;

    public Transform targetPlayer;
    

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        bool isDeath = GameController.GetInstance().IsDeathPlayer();
        bool isPause = GameController.GetInstance().IsPauseMode();
        bool isWin = GameController.GetInstance().IsWinPlayer();

        if (!isDeath && !isPause && !isWin)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        
        MoveCamera();
    }

    // задаем дмижение камеры мышкой
    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 45);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        targetPlayer.Rotate(Vector3.up * mouseX);
    }
}

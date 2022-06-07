using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 cameraRotation;
    [SerializeField] private Transform characterTransform;

    //灵敏度
    public float MouseSensitivity;
    //鼠标旋转角度
    public Vector2 MaxminAngle;

    void Start()
    {
        cameraTransform = transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var tmp_MouseX = Input.GetAxis("Mouse X");
        var tmp_MouseY = Input.GetAxis("Mouse Y");

        cameraRotation.x -= tmp_MouseY * MouseSensitivity;
        cameraRotation.y += tmp_MouseX * MouseSensitivity;

        //旋转角度限制
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, MaxminAngle.x, MaxminAngle.y);

        cameraTransform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, z: 0);

        //跟随视角移动
        characterTransform.rotation = Quaternion.Euler(x: 0, cameraRotation.y, z: 0);
    }
    //private void OnAnimatorMove()
    //{
        
    //}
}

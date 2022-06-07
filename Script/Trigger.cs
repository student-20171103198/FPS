﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///角色控制脚本
public class Player : MonoBehaviour
{
    public Camera camera;
    public Camera cameraPlayer;
    public float m_speed = 1;   //这个是定义的玩家的移动速度  之所以Public是因为为了方便对其进行调节  (public的属性和对象会在Unity中物体的脚本选项中显示出来  前提是你把脚本挂在了物体上)
    public float CameraOffsetX = 6;
    public float CameraOffset_X = -6;
    void Update()   //这个是刷新的意思   以帧为单位的大概每刷新一次1/20秒
    {
        float movex = 0;   //这个代表的是玩家在x轴上的移动
        float movez = 0;   //这个代表的是玩家在z轴上的移动
        if (Input.GetKey(KeyCode.W))   //这个意思是"当按下W键时"
        {
            movez += m_speed * Time.deltaTime;   //物体获得在z轴方向上的增量   也就是向前
        }
        if (Input.GetKey(KeyCode.S))   //按下S键时
        {
            movez -= m_speed * Time.deltaTime;   //后
        }
        if (Input.GetKey(KeyCode.A))   //A键
        {
            movex -= m_speed * Time.deltaTime;    //左
            if (camera.transform.position.x > CameraOffset_X)
            {
                float x = camera.transform.position.x;
                x -= m_speed * Time.deltaTime;
                camera.transform.position = new Vector3(x, camera.transform.position.y, camera.transform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.D))   //D键
        {
            movex += m_speed * Time.deltaTime;   //右
            if (camera.transform.position.x < CameraOffsetX)
            {
                float x = camera.transform.position.x;
                x += m_speed * Time.deltaTime;
                camera.transform.position = new Vector3(x, camera.transform.position.y, camera.transform.position.z);
            }
        }
        this.transform.Translate(new Vector3(movex, 0, movez));   //这句代码是把得到的偏移量通过translate(平移函数)给玩家  从而使得玩家的位置得到改变
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.name);
        cameraPlayer.cullingMask = 1 << 9;
    }
}
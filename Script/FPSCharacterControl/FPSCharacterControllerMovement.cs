using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家状态
public enum PlayerState
{
    idle,
    walk,
    die,
    attack,
    run
}

public class FPSCharacterControllerMovement : MonoBehaviour
{
    public PlayerState currentState;
    private CharacterController characterController;
    private Transform characterTransform;
    private Vector3 movementDirection;
    private Animator characterAnimator;

    public float WalkSpeed;
    public float Gravity = 9.8f;
    public float JumpHeight;
    public float RunSpeed;
    private float current_velocity;

    void Start()
    {
        //初始化
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        characterAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float tmp_CurrentSpeed = WalkSpeed;
        if (characterController.isGrounded)
        {
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");
            movementDirection = characterTransform.TransformDirection(new Vector3(x: tmp_Horizontal, y: 0, z: tmp_Vertical));

            //跳跃
            if (Input.GetButtonDown("Jump"))
            {
                characterAnimator.SetTrigger("Jump");
                movementDirection.y = JumpHeight;
            }

            //奔跑
            tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;
 

            //动画切换
            var tmp_velocity = characterController.velocity;
            tmp_velocity.y = 0;
            current_velocity = tmp_velocity.magnitude;
            //0.25f过度时间
            characterAnimator.SetFloat("Velocity", current_velocity, 0.25f, Time.deltaTime);
        }

        movementDirection.y -= Gravity * Time.deltaTime;
        characterController.Move(tmp_CurrentSpeed * Time.deltaTime * movementDirection);
    }
}

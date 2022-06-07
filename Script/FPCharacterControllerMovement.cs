using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharacterControllerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private Transform characterTransform;
    private Vector3 movementDirection;

    private bool isCrouched;
    private float originHeight;
    private Animator characterAnimator;
    private float current_velocity;

    public float WalkSpeed;
    public float Gravity = 9.8f;
    public float JumpHeight;
    public float RunSpeed;
    public float CrouchSpeed;
    public float CrouchHeight;

    

    // Start is called before the first frame update
    private void Start()
    {
        //初始化
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        originHeight = characterController.height;
        characterAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
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
                movementDirection.y = JumpHeight;
            }

            //下蹲
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                var tmp_CurrentHeight = isCrouched ? originHeight : CrouchHeight;

                StartCoroutine( DoCrouch(tmp_CurrentHeight) );
                isCrouched = !isCrouched;
            }

            if (isCrouched)
            {
                //下蹲行走
                tmp_CurrentSpeed = CrouchSpeed;
            }
            else
            {
                //奔跑
                tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;
            }

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

    //协程
    private IEnumerator DoCrouch(float _target)
    {
        float tmp_CurrentHeight = 0;

        while (Mathf.Abs(characterController.height - _target) > 0.1f)
        {
            yield return null;
            characterController.height = Mathf.SmoothDamp(characterController.height, _target, ref tmp_CurrentHeight, Time.deltaTime * 5);
        }
    }
}

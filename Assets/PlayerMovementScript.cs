using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Ground mode related
    public float walkSpeed;
    public float jumpStregth;
    private Vector3 gravity;

    // Fly mode related
    public bool flyMode;
    public float flySpeed;
    public float jumpTimerDelay;
    private float jumpTimer = -1;

    public Transform viewCamera;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    Animator animator;
    public Transform playerModel;

    // Other private
    private CharacterController charControl;
    private float vertical;

    void Awake()
    {
        gravity = Physics.gravity * 2;
    }

    void Start()
    {
        Cursor.visible = false;
        charControl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        // Horizontal movement vector
        Vector3 direction;
        
        float movementX;
        float movementZ;

        // Ground Mode
        if (!flyMode)
        {
            // GetAxisRaw() for static movement
            movementX = Input.GetAxisRaw("Strafe");
            movementZ = Input.GetAxisRaw("Forward");

            // Jumping from ground
            if (Input.GetButtonDown("Jump"))
            {
                if (charControl.isGrounded) 
                { 
                    vertical = jumpStregth; 
                }
                // Switch to fly mode
                else if (!charControl.isGrounded && vertical > 0)
                {
                    flyMode = true;
                }
            } 
            else
            {
                if (!charControl.isGrounded)
                {
                    vertical += gravity.y * Time.deltaTime;
                }
            }
            animator.SetBool("onGround", charControl.isGrounded);
            animator.SetFloat("Vertical", vertical);

            direction = new Vector3 (movementX, 0, movementZ).normalized;
            if (direction.magnitude > 0.1f) 
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + viewCamera.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

                Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                charControl.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
                animator.SetBool("isWalking", true);
            } else
            {
                animator.SetBool("isWalking", false);
            }
            //bool isShooting = Input.GetButton("Shoot");
            //float modelAngle = Mathf.SmoothDampAngle(playerModel.eulerAngles.y, viewCamera.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            charControl.Move(new Vector3(0, vertical * Time.deltaTime, 0));
        } 
        // Fly mode
        else
        {
            // GetAxis() for smooth movement
            movementX = Input.GetAxisRaw("Strafe");
            movementZ = Input.GetAxisRaw("Forward");
            vertical = Input.GetAxis("Fly") * flySpeed;

            // Switching to ground mode
            if (charControl.isGrounded) { flyMode = false; }
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpTimer < 0)
                {
                    jumpTimer = jumpTimerDelay;
                } else
                {
                    flyMode = false;
                    vertical = -0f;
                }
            }
            jumpTimer -= Time.deltaTime;

            direction = new Vector3(movementX, 0, movementZ).normalized;
            if (direction.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + viewCamera.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

                Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                charControl.Move(moveDirection.normalized * flySpeed * Time.deltaTime);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
            charControl.Move(new Vector3(0, vertical * Time.deltaTime, 0));
        }
        bool isShooting = Input.GetButton("Shoot");
        playerModel.rotation = Quaternion.Euler(0, (isShooting ? viewCamera.eulerAngles.y : transform.eulerAngles.y), 0);
        animator.SetBool("isShooting", isShooting);

        animator.SetBool("FlyMode", flyMode);
    }
}

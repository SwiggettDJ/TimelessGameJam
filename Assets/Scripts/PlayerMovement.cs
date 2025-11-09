using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;
    private float moveSpeed;
    private float walkSpeed = 10f;
    private float sprintSpeed = 20f;
    private Vector3 moveDirection;
    private Vector3 cameraRelativeMovement;
    private float sprintFovDelta = 15f;
    /*private float slowDownMoveSpeed;
    private float groundFriction = 5f;
    private float airFriction = 2f;*/
    private Vector2 horizontalVelocity;

    private float gravity = -40;
    private float groundedGravity = -10f;
    private float jumpHeight = 15f;

    private bool isMovingInput;
    private bool isSprinting;
    

    private PlayerCamManager camManager;
    private Camera mainCamera;
    
    public event Action OnPlayerJump;
    public static event Action<bool> OnPlayerWalk;
    public event Action<bool> OnPlayerSprint;
    public event Action<bool> OnPlayerFall;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        moveSpeed = walkSpeed;
        camManager = GetComponentInChildren<PlayerCamManager>();
        
        //GetComponent<PlayerInput>().enabled = false;
        //camManager.EnableFreeCam(false);
    }

    void Update()
    {
        SetMainCamera();
        HandleRotation();
        HandleMovement();
        ApplyGravity();
    }
    
    private void HandleMovement()
    {
        if (isMovingInput)
        {
            if (playerController.isGrounded)
            {
                OnPlayerWalk?.Invoke(true);
            }

            //slowDownMoveSpeed = moveSpeed;
            
            cameraRelativeMovement = ConvertToCameraSpace(moveDirection);
            cameraRelativeMovement.x *= moveSpeed;
            cameraRelativeMovement.z *= moveSpeed;
            playerController.Move(cameraRelativeMovement * Time.unscaledDeltaTime);
        }
        else
        {
            OnPlayerWalk?.Invoke(false);
            Sprint(false);
            playerController.Move(moveDirection * Time.unscaledDeltaTime);
        }
        
    }

    private void HandleRotation()
    {
        Vector3 lookDirection;
        //Get where we want to face based off of our camera relative movement
        lookDirection.x = cameraRelativeMovement.x;
        lookDirection.y = 0f;
        lookDirection.z = cameraRelativeMovement.z;
        //Get our direction in quaternions
        Quaternion currentRotation = transform.rotation;

        //Check that any move input is pressed
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            //This if statement stops Quaternion.LookRotation from printing to console every time the vector is zero
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                //Slerp between our old ratation and the target at a given speed
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.unscaledDeltaTime * 10f);
            }
        }
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float yValue = vectorToRotate.y;
        
        Vector3 cameraFoward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        
        cameraFoward.y = 0;
        cameraRight.y = 0;
        
        cameraFoward.Normalize();
        cameraRight.Normalize();

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraFoward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;
        
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = yValue;
        return vectorRotatedToCameraSpace;
    }

    private void OnMovement(InputValue value)
    { 
        Vector2 horizontalInput = value.Get<Vector2>().normalized;
        moveDirection = new Vector3(horizontalInput.x, moveDirection.y, horizontalInput.y);
        isMovingInput = horizontalInput != Vector2.zero;
        
        //If we let go of movement input we stop sprinting
        if (!isMovingInput)
        {
            Sprint(false);
        }
    }

    //set to broadcast on press and release in InputActions
    private void OnJump(InputValue value)
    {
        if (value.isPressed && playerController.isGrounded)
        {
            moveDirection.y = jumpHeight;
            OnPlayerJump?.Invoke();
        }
    }

    //Toggles Sprinting on and off when input is received
    private void OnSprint(InputValue value)
    {
        if (isMovingInput)
        {
            Sprint(!isSprinting);
        }
    }

    /*private void MoveWithFriction(float friction)
    {
        if (slowDownMoveSpeed <= 0.05f) slowDownMoveSpeed = 0;
        
        Vector3 slowDownDirection = new Vector3(transform.forward.x, moveDirection.y, transform.forward.z);
        slowDownDirection.x *= slowDownMoveSpeed;
        slowDownDirection.z *= slowDownMoveSpeed;
        playerController.Move(slowDownDirection * Time.unscaledDeltaTime);

        slowDownMoveSpeed = Mathf.Lerp(slowDownMoveSpeed, 0, Time.unscaledDeltaTime * friction);
    }*/

    private void ApplyGravity()
    {
        if (playerController.isGrounded)
        {
            moveDirection.y = groundedGravity;
            OnPlayerFall?.Invoke(false);
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
            OnPlayerFall?.Invoke(true);
        }
    }

    private void Sprint(bool shouldSprint)
    {
        //Can't stop or start sprinting in the air
        if (!playerController.isGrounded)
        {
            return;
        }
        
        //Only sprint if we weren't sprinting and vice versa
        if (shouldSprint != isSprinting)
        {
            if (shouldSprint)
            {
                moveSpeed = sprintSpeed;
                camManager.ChangeFov(sprintFovDelta);
            }
            else if (!shouldSprint)
            {
                moveSpeed = walkSpeed;
                camManager.ChangeFov(-sprintFovDelta);
            }
            isSprinting = shouldSprint;
            OnPlayerSprint?.Invoke(shouldSprint);
        }
    }


    /* Grabs main camera because we need the camera world space transform values for converting to camera space but the freelookcam
     is our child. Since the player isn't destroyed on scene load we need to find main camera every time its deleted.
    (finding main cam using SceneManager.sceneLoaded doesn't work for some reason) */
    private void SetMainCamera()
    {
        if (mainCamera == null)
        {
            //print("Camera Found");
            mainCamera = Camera.main;
        }
    }


}

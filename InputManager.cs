using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    AnimatorManager animatorManager;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;
    CharacterController controller;

    public Vector2 input;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float scroll;

    [Header("Indicator")]
    public bool ShiftPress;
    public bool SpacePress;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController = new PlayerController();

            playerController.PlayerMovement.Movement.performed += i => input = i.ReadValue<Vector2>();
            //shift
            playerController.PlayerMovement.Runing.performed += i => ShiftPress = true;
            playerController.PlayerMovement.Runing.canceled += i => ShiftPress = false;

            //jump
            playerController.PlayerMovement.Jump.performed += i => SpacePress = true;
            playerController.PlayerMovement.Jump.canceled += i => SpacePress = false;

            
        }

        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    public void HandleAllInput()
    {
        MovementInput();
        HandleSprintingInput();
        HandleJumpInput();
    }

    private void MovementInput()
    {
        horizontal = input.x;
        vertical = input.y;

        //start animasi
        //rumus menghitung, buat animasi
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpadateAnimatorValue(0f, moveAmount, playerLocomotion.lari);

        //animasi after lari, biar g garing animasinya :)
        if(moveAmount == 0 && playerLocomotion.percepatanLari > 0)
        {
            animatorManager.UpadateAnimatorValue(0f, playerLocomotion.percepatanLari, false);
            Debug.Log("YES");
        }
        //Debug.Log(moveAmount);
        //Debug.Log(controller.isGrounded);
    }

    private void HandleSprintingInput()
    {
        if (ShiftPress && moveAmount == 1)
        {
            playerLocomotion.lari = true;
            //tambahkan UpdatValueAnimatio pengkondisan bool
        }
        else
        {
            playerLocomotion.lari = false;
        }
    }

    private void HandleJumpInput()
    {
        if (SpacePress)
        {
            playerLocomotion.loncat = true;
        }
        else
        {
            playerLocomotion.loncat = false;
        }
    }

    
}

    PlayerLocomotion playerLocomotion;

    public Vector2 input;
    public float horizontal;
    public float vertical;
    public float moveAmount;

    [Header("Indicator")]
    public bool ShiftPress;
    public bool SpacePress;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController = new PlayerController();

            playerController.PlayerMovement.Movement.performed += i => input = i.ReadValue<Vector2>();
            //shift
            playerController.PlayerMovement.Runing.performed += i => ShiftPress = true;
            playerController.PlayerMovement.Runing.canceled += i => ShiftPress = false;

            //jump
            playerController.PlayerMovement.Jump.performed += i => SpacePress = true;
            playerController.PlayerMovement.Jump.canceled += i => SpacePress = false;
        }

        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    public void HandleAllInput()
    {
        MovementInput();
        HandleSprintingInput();
        HandleJumpInput();
    }

    private void MovementInput()
    {
        horizontal = input.x;
        vertical = input.y;

        //start animasi
        //rumus menghitung, buat animasi
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpadateAnimatorValue(0f, moveAmount, playerLocomotion.lari);
        //Debug.Log(moveAmount);
    }

    private void HandleSprintingInput()
    {
        if (ShiftPress && moveAmount == 1)
        {
            playerLocomotion.lari = true;
            //tambahkan UpdatValueAnimatio pengkondisan bool
        }
        else
        {
            playerLocomotion.lari = false;
        }
    }

    private void HandleJumpInput()
    {
        if (SpacePress)
        {
            playerLocomotion.loncat = true;
        }
        else
        {
            playerLocomotion.loncat = false;
        }
    }

    
}

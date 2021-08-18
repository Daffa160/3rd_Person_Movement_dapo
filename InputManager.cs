using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;

    public Vector2 input;
    public float horizontal;
    public float vertical;
    public float moveAmount;

    [Header("Indicator")]
    public bool ShiftPress;

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
        HandleSprintingMovement();
    }

    private void MovementInput()
    {
        horizontal = input.x;
        vertical = input.y;

        //start animasi
        //rumus menghitung, buat animasi
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpadateAnimatorValue(0f, moveAmount, playerLocomotion.lari);
        Debug.Log(moveAmount);
    }

    private void HandleSprintingMovement()
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
}

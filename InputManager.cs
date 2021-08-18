using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;

    public Vector2 input;//butuh Variabel arah gerak x y
    public float horizontal;
    public float vertical;


    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController = new PlayerController();
            //mengaktifkan inputActionMap (new input system )
            playerController.PlayerMovement.Movement.performed += i => input = i.ReadValue<Vector2>();
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
    }

    private void MovementInput()
    {
        horizontal = input.x;
        vertical = input.y;
    }
}

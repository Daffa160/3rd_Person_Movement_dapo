using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //referensi buat input system
    PlayerController playerController;

    //butuh variabel buat wasd (Vector2)
    [Header("Inputan User")]
    public Vector2 arahGerak;
    public float vertical;
    public float horizontal;


    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController = new PlayerController();

            playerController.PlayerMovement.Movement.performed += i => arahGerak = i.ReadValue<Vector2>();
        }

        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    public void HandleAllInput()
    {
        Movement();
    }

    private void Movement()
    {
        vertical = arahGerak.y;
        horizontal = arahGerak.x;
    }


}

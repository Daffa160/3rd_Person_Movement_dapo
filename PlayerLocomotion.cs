using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    CharacterController controller;

    [Header("Movement Speed")]

    //butuh variabel tubuh player
    //Rigidbody playerRigidbody;
    //butuh variabel obyek untuk digerakan
    Transform cameraObyek; // sesuai tampilan kamerea arah graknya

    //variabel arah
    Vector3 direction; //butuh variabel arah x, y, z yang akan dituju

    public float walkSpeed = 0.03f;
    public float runSpeed = 0.01f;
    public float sprintSpeed = 10;

    [Header("Kecepatan Rotasi")]
    public float rotationSpeed = 15;

    [Header("Indicator")]
    public bool lari; //untuk pengkondisian true or false nya ada pada inputManager


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        //playerRigidbody = GetComponent<Rigidbody>();
        cameraObyek = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        Movement();
        RotationMovement();
    }

    private void Movement()
    {
        //rumus inputan buat vertical dan hori
        direction = cameraObyek.forward * inputManager.vertical;
        direction = direction + cameraObyek.right * inputManager.horizontal;
        direction.Normalize(); // di normalkan jika inputanya melebihi dari 2
        direction.y = 0;

        // kecepatan maju obyek
        if (lari)
        {
            direction = direction * sprintSpeed;
        }
        else
        {
            if(inputManager.moveAmount >= 0.5)
            {
                direction = direction * runSpeed;
            }
            else
            {
                direction = direction * walkSpeed;
            }
        }
        

        //rumus untuk menggerakn obyek
        Vector3 movement = direction;
        //playerRigidbody.velocity = movement;
        controller.Move(movement);
    }

    private void RotationMovement()
    {
        //dibutuhkan Variabel Vector baru untuk penghitung
        Vector3 targetRotation = Vector3.zero;
        //rumusnya hampir sama dengan movement()
        targetRotation = cameraObyek.forward * inputManager.vertical;
        targetRotation = targetRotation + cameraObyek.right * inputManager.horizontal;
        targetRotation.Normalize();
        targetRotation.y = 0;

        //jika arah kamera == 0 akan bergerak maju kedepan
        if(targetRotation == Vector3.zero)
        {
            targetRotation = transform.forward;
        }

        //Quaternion bertanggung jawab untuk menghitung nilai2 Vector3, biasanya utk menghitung pergesaran rotasi
        //rumus rotasi obyek
        Quaternion targetDirection = Quaternion.LookRotation(targetRotation);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetDirection, rotationSpeed * Time.deltaTime);

        //baru di gerakan dan rotasikan
        transform.rotation = playerRotation;
    }

}

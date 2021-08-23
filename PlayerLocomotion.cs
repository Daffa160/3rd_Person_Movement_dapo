using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    CharacterController controller;

    [Header("Movement Speed")]

    //butuh variabel tubuh player
    Rigidbody playerRigidbody;
    //butuh variabel obyek untuk digerakan
    Transform cameraObyek; // sesuai tampilan kamerea arah graknya

    //variabel arah
    Vector3 direction; //butuh variabel arah x, y, z yang akan dituju

    [Header("Movement Speed")]
    public float walkSpeed = 0.03f;
    public float runSpeed = 0.01f;
    public float sprintSpeed = 0.015f;
    public float percepatanLari;

    [Header("Kecepatan Rotasi")]
    public float rotationSpeed = 8;

    [Header("Grafitasi")]
    Vector3 grevityVelocity;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool diTanah;
    public float inAirTime;

    [Header("Jump")]
    public float gravity = -15;
    public float jumpHeight = 3;

    [Header("Indicator")]
    public bool lari; //untuk pengkondisian true or false nya ada pada inputManager
    public bool loncat;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObyek = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        Movement();
        RotationMovement();
        JumpMovement();
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
            percepatanLari = percepatanLari + Time.deltaTime;
            if(percepatanLari > 1f)
            {
                percepatanLari = 1f;
            }
            direction = direction * sprintSpeed;
        }
        else
        {
            percepatanLari = percepatanLari - Time.deltaTime;
            if(percepatanLari < 0)
            {
                percepatanLari = 0f;
            }

            if (inputManager.moveAmount >= 0.5)
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
        playerRigidbody.velocity = movement;
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
        if (targetRotation == Vector3.zero)
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

    private void JumpMovement()
    {
        diTanah = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (diTanah && grevityVelocity.y < 0)
        {
            grevityVelocity.y = -2f;
        }

        if (loncat && diTanah)
        {
            grevityVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        grevityVelocity.y += gravity * Time.deltaTime;
        controller.Move(grevityVelocity * Time.deltaTime);
    }


}

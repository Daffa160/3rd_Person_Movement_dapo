using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    //variabel untuk lokasi arah gerak
    Vector3 direction;
    //variabel untuk obyek bergerak, kita ingin obyek bergerak sesuai arah kamera
    Transform Arahcamera;

    [Header("Movement Speed")]
    public float walkingSpeed = 1;
    public float runingSpeed = 4;
    public float sprintingSpeed = 7;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        //karena kita ingin menggerakan player dengan arah kamera, di awal perlu di transform
        Arahcamera = Camera.main.transform;
    }

    public void SemuaGerakan()
    {
        ArahBergerak();
    }


    //void aksi terhadap inputan
    private void ArahBergerak()
    {
        direction = Arahcamera.forward * inputManager.vertical;
        direction = direction + Arahcamera.right * inputManager.horizontal;
        direction.Normalize(); // kita ingin inputan sesuai ketika inputan lebih dari 2

        direction.y = 0;
        direction = direction * walkingSpeed;
    }

}

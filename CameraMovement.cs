using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject character;
    public GameObject cameraCenter;
    public float yOffset = 1f;
    public float sensitive = 10f;
    public Camera cam;

    [Header("Dampening")]
    public float scrollSensitivity = 5f;
    public float scrollDamening = 6f;

    [Header("Zoom camera")]
    public float zoomMin = 3f;
    public float zoomMax = 15f;
    public float zoomDefault = 10f;
    public float zoomDistance;

    [Header("Collision")]
    public float collisionSensitivity = 4.5f;

    [Header("Indicator")]
    public bool dibalik;
    public bool cursor;

    private RaycastHit _camHit;
    private Vector3 _camDist;

    private void Start()
    {
        _camDist = cam.transform.localPosition;
        zoomDistance = zoomDefault;
        _camDist.z = zoomDistance;

        //cursor
        if (cursor)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");
        float scrrol = Input.GetAxis("Mouse ScrollWheel");

        cameraCenter.transform.position = new Vector3(character.transform.position.x,
            character.transform.position.y + yOffset, character.transform.position.z);

        var rotation = Quaternion.Euler(cameraCenter.transform.rotation.eulerAngles.x - (-mouseY * sensitive / 2),
            cameraCenter.transform.rotation.eulerAngles.y - (-mouseX * sensitive),
            cameraCenter.transform.rotation.eulerAngles.z);
        cameraCenter.transform.rotation = rotation;

        

        //scroll mouse // zoom
        if (scrrol != 0f)
        {
            var scroolAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            scroolAmount *= zoomDistance * 0.3f;
            zoomDistance += -scroolAmount;
            zoomDistance = Mathf.Clamp(zoomDistance, zoomMin, zoomMax);
        }

        if (_camDist.z != -zoomDistance)
        {
            _camDist.z = Mathf.Lerp(_camDist.z, -zoomDistance, scrollDamening * Time.deltaTime);
        }

        cam.transform.localPosition = _camDist;

        //collision

        GameObject obj = new GameObject();
        obj.transform.SetParent(cam.transform.parent);
        obj.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z - collisionSensitivity);


        if(Physics.Linecast(cameraCenter.transform.position, obj.transform.position, out _camHit))
        {
            cam.transform.position = _camHit.point;

            var localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z + collisionSensitivity);

            cam.transform.localPosition = localPosition;
        }
        Destroy(obj);

        if(cam.transform.localPosition.z > -1f)
        {
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    public float batasBawah = -50f;
    public float batasAtas = 70f;

    private void Update()
    {
        var rotation = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform);

        if(rotation.x < batasBawah)
        {
            UnityEditor.TransformUtils.SetInspectorRotation(gameObject.transform, new Vector3(batasBawah, rotation.y, rotation.z));
        }
        
        if(rotation.x > batasAtas)
        {
            UnityEditor.TransformUtils.SetInspectorRotation(gameObject.transform, new Vector3(batasAtas, rotation.y, rotation.z));
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarder : MonoBehaviour
{
    public bool yOnly;
    Transform cameraTr;
    void Start()
    {
        cameraTr = Camera.main.transform;
        Update();
    }

    void Update()
    {
        if (yOnly)
        {
            transform.LookAt(cameraTr);
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Euler(rotation);
        }
        else
            transform.forward = cameraTr.forward;
    } 
}

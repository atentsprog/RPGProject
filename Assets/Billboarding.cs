using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{

    Transform cameraTr;
    void Start()
    {
        cameraTr = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTr);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
    [ContextMenu("SetBillboarding")]
    void SetBillboarding()
    {
        Start();
        Update();
    }
}

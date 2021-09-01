using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePositionY : MonoBehaviour
{
    public float y = 0.5f;
    void Start()
    {
        transform.Translate(0, y, 0, Space.World);
    }
}

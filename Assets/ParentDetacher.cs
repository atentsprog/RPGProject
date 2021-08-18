using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDetacher : MonoBehaviour
{
    void Awake()
    {
        transform.SetParent(null);
    }
}

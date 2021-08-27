using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalIndicator : MonoBehaviour
{
    Collider terrainCollider;
    private void Awake()
    {
        terrainCollider = Terrain.activeTerrain.GetComponent<Collider>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftAlt))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(terrainCollider.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                transform.position = hit.point;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalIndicator : MonoBehaviour
{
    public GameObject indicatorPrefab;
    Collider terrainCollider;
    void Start()
    {
        //goTerrain = Terrain.activeTerrain;
        terrainCollider = Terrain.activeTerrain.GetComponent<Collider>();
    }
    public float addHeight = 0.5f;

    public LayerMask indicator3DEffectLayer;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftAlt))
        {
            print("Alt + 클릭함");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, float.MaxValue, indicator3DEffectLayer))
            {
                Destroy(hit.transform.root.gameObject);
            }
            else
            { 
                if (terrainCollider.Raycast(ray, out hit, Mathf.Infinity))
                {
                    transform.position = hit.point + new Vector3(0, addHeight, 0);
                    Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}

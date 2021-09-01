using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalIndicator : MonoBehaviour
{
    public Collider terrainCollider;
    public GameObject indicatePrefab;
    public LayerMask indicatorLayer;
    private void Awake()
    {
        if (terrainCollider == null)
        {
            var activeTerrain = Terrain.activeTerrain;
            if(activeTerrain != null)
                terrainCollider = activeTerrain.GetComponent<Collider>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftAlt))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            bool hitIndicator = false;
            if (Physics.Raycast(ray, out RaycastHit hit1, float.MaxValue, indicatorLayer))
            {
                if(hit1.transform.CompareTag("Indicator"))
                {
                    hitIndicator = true;

                    Destroy(hit1.transform.gameObject); // 기존 인디케이터 삭제로직.
                }
            }

            if (hitIndicator == false)
            {
                if (terrainCollider.Raycast(ray, out RaycastHit hit2, float.MaxValue))
                {
                    Instantiate(indicatePrefab
                        , hit2.point + new Vector3(0, 0.5f, 0)
                        , Quaternion.identity);
                }
            }
        }
    }
}

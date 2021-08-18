using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxMaterialControll : MonoBehaviour
{
    public Material skyBoxMaterial;
    public float speed = 1f;
    public float currentRotation = 0;
    IEnumerator Start()
    {
        
        while (true)
        {
            currentRotation += speed * Time.deltaTime;
            skyBoxMaterial.SetFloat("_Rotation", currentRotation);

            yield return null;
        }
    }
    private void OnDestroy()
    {
        skyBoxMaterial.SetFloat("_Rotation", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTranslate : MonoBehaviour
{
    public Vector3 noise = new Vector3(0.05f, 0.05f, 0.05f);
    public float translateTime = 0.1f;
    IEnumerator Start()
    {
        Vector3 originalPos = transform.position;
        while(true)
        {
            transform.position = originalPos + new Vector3(
                Random.Range(-noise.x, noise.x),
                Random.Range(-noise.y, noise.y),
                Random.Range(-noise.z, noise.z));                

            yield return new WaitForSeconds(Random.Range(0, translateTime));
        }
    }
}

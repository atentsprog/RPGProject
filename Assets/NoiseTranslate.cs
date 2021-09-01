using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTranslate : MonoBehaviour
{
    public Vector3 move = new Vector3(0.1f, 0.1f, 0.1f);
    public float randomTransitionTime = 0.1f;
    IEnumerator Start()
    {
        Vector3 originalPosition = transform.position;
        while(true)
        {
            transform.position = originalPosition + new Vector3(
                Random.Range(-move.x, move.x),
                Random.Range(-move.y, move.y),
                Random.Range(-move.z, move.z));

            yield return new WaitForSeconds(Random.Range(0, randomTransitionTime));
        }
    }
}

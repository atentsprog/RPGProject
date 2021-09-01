using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    public float destroyDelay = 3;
    public float duration = 1;

    void Start()
    {
        Light light = GetComponent<Light>();
        DOTween.To( () => light.intensity, x => light.intensity = x, 0, duration)
            .SetDelay(destroyDelay);
    }
}

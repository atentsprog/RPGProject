using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestroyer : MonoBehaviour
{   
    public float duration = 0.5f;
    public float delay = 4;

    void Start()
    {
        Light light = GetComponent<Light>();
        DOTween.To(() => light.intensity, x => light.intensity = x, 0, duration)
            .SetDelay(delay)
            .OnComplete( () => light.enabled = false)
            .SetLink(gameObject);
    }
}

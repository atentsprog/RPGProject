using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float duration = 0.5f;
    IEnumerator Start()
    {
        Light light = GetComponent<Light>();

        float originalRange = light.range;
        TweenerCore<float, float, FloatOptions> tween = null;
        while (true)
        {
            float _duratio =  Random.Range(0, duration);
            tween = DOTween.To(() => light.range, x => light.range = x, originalRange * Random.Range(minScale, maxScale)
                , _duratio)
                .SetLink(gameObject);

            yield return new WaitForSeconds(_duratio);
            tween.Kill();
        }
    }
}

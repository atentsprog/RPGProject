using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public bool destroyOnEndPlaying = true;
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
    }
    private void Update()
    {
        if (destroyOnEndPlaying && ps.isPlaying == false)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletDecal = null;
    public float speed = 50f;
    float timeToDestroy = 3f;
    public Vector3 target;
    public bool hit;
    public Vector3 targetContactNormal;
    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            if (hit)
                Instantiate(bulletDecal, target + targetContactNormal * 0.0001f, Quaternion.LookRotation(targetContactNormal));

            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        var contact = other.GetContact(0);
        Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}

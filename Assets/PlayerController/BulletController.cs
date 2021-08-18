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
        if (isDestroyed)
        {
            Debug.LogWarning("이미 삭제 되었음, 여긴 절대 안올꺼야!");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            print("위치까이 와서 터짐" + transform.position.ToString());
            if (hit)
                Instantiate(bulletDecal, target + targetContactNormal * 0.001f, Quaternion.LookRotation(targetContactNormal));

            Destroy(gameObject);
            isDestroyed = true;
        }
    }
    bool isDestroyed = false;
    private void OnCollisionEnter(Collision other)
    {
        if (isDestroyed)
        {
            Debug.LogWarning("이미 충돌 해서 삭제 했는데 충돌함");
            return;
        }
        var contact = other.GetContact(0);
        print($"충돌해서 터짐, {contact.point}, {contact.point.z}");
        Instantiate(bulletDecal, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal));
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

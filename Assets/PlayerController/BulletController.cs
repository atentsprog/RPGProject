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
            Debug.LogWarning("�̹� ���� �Ǿ���, ���� ���� �ȿò���!");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            print("��ġ���� �ͼ� ����" + transform.position.ToString());
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
            Debug.LogWarning("�̹� �浹 �ؼ� ���� �ߴµ� �浹��");
            return;
        }
        var contact = other.GetContact(0);
        print($"�浹�ؼ� ����, {contact.point}, {contact.point.z}");
        Instantiate(bulletDecal, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal));
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

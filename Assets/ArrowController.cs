using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour, IProjectile
{
    public Vector3 Target { get => target; set => target = value; }
    public bool Hit { get => hit; set => hit = value; }
    public Vector3 TargetContactNormal { get => targetContactNormal; set => targetContactNormal = value; }

    Vector3 target;
    bool hit;
    Vector3 targetContactNormal;

    [SerializeField] GameObject bulletDecal = null;
    public float force = 50f;
    float timeToDestroy = 3f;

    new Rigidbody rigidbody;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);

        Vector3 toDirec = (target - transform.position).normalized;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(toDirec * force, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (isDestroyed)
        {
            Debug.LogWarning("�̹� ���� �Ǿ���, ���� ���� �ȿò���!");
            return;
        }

        if(rigidbody.velocity != Vector3.zero)
            transform.forward = rigidbody.velocity;


        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            //print("��ġ���� �ͼ� ����" + transform.position.ToString());
            if (hit)
                Instantiate(bulletDecal, target, previousRotation);

            Destroy(gameObject);
            isDestroyed = true;
        }
        previousRotation = transform.rotation;
    }
    Quaternion previousRotation;
    bool isDestroyed = false;
    private void OnCollisionEnter(Collision other)
    {
        if (isDestroyed)
        {
            //Debug.LogWarning("�̹� �浹 �ؼ� ���� �ߴµ� �浹��");
            return;
        }
        var contact = other.GetContact(0);
        //print($"�浹�ؼ� ����, {contact.point}, {contact.point.z}");
        Instantiate(bulletDecal, contact.point, previousRotation);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

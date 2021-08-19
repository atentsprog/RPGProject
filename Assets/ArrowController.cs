using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour, IProjectile
{
    public Vector3 Target { get => target; set => target = value; }
    public bool Hit { get => hit; set => hit = value; }
    public Vector3 TargetContactNormal { get => targetContactNormal; set => targetContactNormal = value; }
    public float CurrentAngle { get => currentAngle; set => currentAngle = value; }

    Vector3 target;
    bool hit;
    Vector3 targetContactNormal;
    float currentAngle;

    [SerializeField] GameObject bulletDecal = null;
    public float force = 50f;
    float timeToDestroy = 3f;

    new Rigidbody rigidbody;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);

        rigidbody = GetComponent<Rigidbody>();

        //���� �ڵ� �������� ���ư�.
        //Vector3 toDirec = (target - transform.position).normalized;
        //rigidbody.AddForce(toDirec * force, ForceMode.VelocityChange);


        Vector3 direction = target - transform.position;
        float yOffset = direction.y;
        direction = ProjectileMath.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        bool targetInRange = ProjectileMath.LaunchAngle(force, distance, yOffset
            , Physics.gravity.magnitude, out float angle0, out float angle1);
        if (targetInRange)
            currentAngle = angle1;
        rigidbody = GetComponent<Rigidbody>();

        transform.forward = direction;
        float degree = -currentAngle * Mathf.Rad2Deg;
        transform.Rotate(degree, 0, degree);
        rigidbody.velocity = rigidbody.transform.forward * force;
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
        //print($"�浹�ؼ� ����, {contact.point}, {contact.point.y}");
        Instantiate(bulletDecal, contact.point, previousRotation);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

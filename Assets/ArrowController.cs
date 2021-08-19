using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour, IProjectile
{
    public Vector3 Target { get => target; set => target = value; }
    public bool Hit { get => hit; set => hit = value; }
    public Vector3 TargetContactNormal { get => targetContactNormal; set => targetContactNormal = value; }
    public float Speed { get => force; set => force = value; }
    public float CurrentAngle { get => currentAngle; set => currentAngle = value; }
    float currentAngle;

    Vector3 target;
    bool hit;
    Vector3 targetContactNormal;

    [SerializeField] GameObject bulletDecal = null;
    public float force = 50f;
    float timeToDestroy = 3f;

    new Rigidbody rigidbody;
    //public float distance = 30;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);

        Vector3 direction = target - transform.position;
        float yOffset = direction.y;
        direction = ProjectileMath.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        bool targetInRange = ProjectileMath.LaunchAngle(force, distance, yOffset, Physics.gravity.magnitude, out float angle0, out float angle1);
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
            Debug.LogWarning("이미 삭제 되었음, 여긴 절대 안올꺼야!");
            return;
        }

        if(rigidbody.velocity != Vector3.zero)
            transform.forward = rigidbody.velocity;


        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            //print("위치까이 와서 터짐" + transform.position.ToString());
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
            //Debug.LogWarning("이미 충돌 해서 삭제 했는데 충돌함");
            return;
        }
        var contact = other.GetContact(0);
        //print($"충돌해서 터짐, {contact.point}, {contact.point.y}");
        Instantiate(bulletDecal, contact.point, previousRotation);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

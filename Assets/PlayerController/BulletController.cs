using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IProjectile
{
    public Vector3 Target { get; set; }
    public bool Hit { get; set; }
    public Vector3 TargetContactNormal { get; set; }
    public float Speed { get; set; }
    public float CurrentAngle { get; set; }
}

public class BulletController : MonoBehaviour, IProjectile
{
    public Vector3 Target { get => target; set => target = value; }
    public bool Hit { get => hit; set => hit = value; }
    public Vector3 TargetContactNormal { get => targetContactNormal; set => targetContactNormal = value; }
    public float Speed { get => speed; set => speed = value; }
    public float CurrentAngle { get => 0; set { } }

    Vector3 target;
    bool hit;
    Vector3 targetContactNormal;

    [SerializeField] GameObject bulletDecal = null;
    public float speed = 50f;
    float timeToDestroy = 3f;

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

        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, Target) < 0.01f)
        {
            print("위치까이 와서 터짐" + transform.position.ToString());
            if (Hit)
                Instantiate(bulletDecal, Target, Quaternion.LookRotation(TargetContactNormal));

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
        Instantiate(bulletDecal, contact.point, Quaternion.LookRotation(contact.normal));
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        isDestroyed = true;
    }
}

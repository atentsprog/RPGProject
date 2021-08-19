using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArcDrawer : MonoBehaviour
{
    public ProjectileArc projectileArc;
    public Transform firePoint;
    public Transform cameraTransform;
    public float speed = 20;
    void Start()
    {
        projectileArc = GetComponent<ProjectileArc>();
        firePoint = transform;
        cameraTransform = Camera.main.transform;
    }
    public LayerMask bulletColllisionDetact = int.MaxValue;
    public float bulletHitMissDistance = 25;
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        Vector3 targetPoint;


        Vector3 rayStartPoint = cameraTransform.position;
        Vector3 cameraPositionSameY = cameraTransform.position;
        cameraPositionSameY.y = transform.position.y;
        float playerDistance = Vector3.Distance(cameraPositionSameY, transform.position);
        rayStartPoint += cameraTransform.forward * playerDistance;

        if (Physics.Raycast(rayStartPoint, cameraTransform.forward
            , out RaycastHit hit, Mathf.Infinity, bulletColllisionDetact))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance; ;
        }

        SetTargetWithSpeed(targetPoint, speed);
    }

    public void ThrowObject()
    {
        //수류탄 생성.
        //리지드 바디에 포스를 줘서 날리자.
        var newGrenadeGo = Instantiate(grenadeGo, firePoint.position, Quaternion.identity);
        newGrenadeGo.transform.forward = direction;
        float degree = -currentAngle * Mathf.Rad2Deg;
        newGrenadeGo.transform.Rotate(degree, 0, degree);
        Rigidbody _rigidbody = newGrenadeGo.GetComponent<Rigidbody>();
        _rigidbody.velocity = newGrenadeGo.transform.forward * speed;

        StartCoroutine(ProjectileArcOffAndOnCo());
    }

    public float offTime = 0.5f; // 다시 사용하는 딜레이 시간 만큼 꺼주자.
    private IEnumerator ProjectileArcOffAndOnCo()
    {
        var lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(offTime);
        lineRenderer.enabled = true;
    }

    public GameObject grenadeGo;
    public float currentAngle;
    Vector3 direction;
    public void SetTargetWithSpeed(Vector3 point, float speed)
    {
        direction = point - firePoint.position;
        float yOffset = direction.y;
        direction = ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;
        //float angle0, angle1;
        bool targetInRange = LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out float angle0, out float angle1);
        if (targetInRange)
            currentAngle = angle1;
        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
    }
    public Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector)
    {
        return vector - (Vector3.Dot(vector, planeNormal) * planeNormal);
    }
    bool LaunchAngle(float speed, float distance, float yOffset, float gravity, out float angle0, out float angle1)
    {
        angle0 = angle1 = 0;

        float speedSquared = speed * speed;

        float operandA = Mathf.Pow(speed, 4);
        float operandB = gravity * (gravity * (distance * distance) + (2 * yOffset * speedSquared));

        // Target is not in range
        if (operandB > operandA)
            return false;

        float root = Mathf.Sqrt(operandA - operandB);

        angle0 = Mathf.Atan((speedSquared + root) / (gravity * distance));
        angle1 = Mathf.Atan((speedSquared - root) / (gravity * distance));

        return true;
    }

}
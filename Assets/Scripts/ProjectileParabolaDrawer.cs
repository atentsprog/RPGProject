using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParabolaDrawer : MonoBehaviour
{
    public ProjectileParabola projectileArc;
    public Transform firePoint;
    public float speed = 20;


    private float bulletHitMissDistance = 25f;
    Transform cameraTransform;
    public LayerMask bulletColllisionDetact = int.MaxValue;

    void Start()
    {
        projectileArc = GetComponent<ProjectileParabola>();
        firePoint = transform;
        cameraTransform = Camera.main.transform;
    } 
    void Update()
    {
        Vector3 targetPoint;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward
            , out RaycastHit hit, Mathf.Infinity, bulletColllisionDetact))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
        }

        SetTargetWithSpeed(targetPoint, speed);
    }

    public GameObject grenadeGo;
    public float currentAngle;
    Vector3 direction;
    public void SetTargetWithSpeed(Vector3 point, float speed)
    {
        direction = point - firePoint.position;
        float yOffset = direction.y;
        direction = ProjectileMath.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;
        float angle0, angle1;
        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out angle0, out angle1);
        if (targetInRange)
            currentAngle = angle0;
        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
    }
}
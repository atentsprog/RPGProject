using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParabolaDrawer : MonoBehaviour
{
    public ProjectileParabola projectileArc;
    public Transform firePoint;
    public float Speed
    {
        get { return speed; }
        set {
            if (speed == value)
                return;
            Debug.Log($"{speed} => {value}");

            speed = value; 
        }
    }
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
        Vector3 rayStartPoint = GetRayStartPoint(cameraTransform, transform);

        if (Physics.Raycast(rayStartPoint, cameraTransform.forward
            , out RaycastHit hit, Mathf.Infinity, bulletColllisionDetact))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
        }

        SetTargetWithSpeed(targetPoint, Speed);
    }

    public static Vector3 GetRayStartPoint(Transform cameraTransform, Transform transform)
    {
        Vector3 rayStartPoint = cameraTransform.position;
        Vector3 cameraPositionSameY = cameraTransform.position;
        cameraPositionSameY.y = transform.position.y;
        float playerDistance = Vector3.Distance(cameraPositionSameY, transform.position);
        rayStartPoint += cameraTransform.forward * playerDistance;
        return rayStartPoint;
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
        //float angle0, angle1;
        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude
            , out float angle0, out float angle1);
        if (targetInRange)
            currentAngle = angle1;
        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
    }
}
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

    public GameObject grenadeGo;
    public float currentAngle;
    Vector3 direction;
    public void SetTargetWithSpeed(Vector3 point, float speed)
    {
        direction = point - firePoint.position;
        float yOffset = direction.y;
        direction = ProjectileMath.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out float angle0, out float angle1);
        if (targetInRange)
            currentAngle = angle1;
        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
    }

    //[ContextMenu("색바꾸기 테스트")]
    //void ChangeColorTest()
    //{
    //    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    //    lineRenderer.material.color = Color.blue;
    //}
}
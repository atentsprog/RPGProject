using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public string parameterAttack = "StartFire";
    public string parameterSpeed = "Speed";
    public string parameterIsMoving = "IsMoving";
    public PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    Transform cameraTransform;
    Animator animator;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        shootAction.performed += ShootAction_performed;
    }

    public GameObject bulletPrefab;
    public GameObject barrelTransform;
    public Transform bulletParent;
    private float bulletHitMissDistance = 25f;

    public LayerMask bulletColllisionDetact = int.MaxValue;
    private void ShootAction_performed(InputAction.CallbackContext obj)
    {
        animator.SetTrigger(parameterAttack);

        GameObject bullet = Instantiate(bulletPrefab, barrelTransform.transform.position
            , Quaternion.LookRotation(cameraTransform.forward), bulletParent);

        var bulletController = bullet.GetComponent<IProjectile>();

        Vector3 rayStartPoint = cameraTransform.position;
        Vector3 cameraPositionSameY = cameraTransform.position;
        cameraPositionSameY.y = transform.position.y;
        float playerDistance = Vector3.Distance(cameraPositionSameY, transform.position);
        rayStartPoint += cameraTransform.forward * playerDistance;

        if (Physics.Raycast(rayStartPoint, cameraTransform.forward
            , out RaycastHit hit, Mathf.Infinity, bulletColllisionDetact))
        {
            //print($"���⿡ ���� �浹��, {hit.point}, {hit.point.z}");
            bullet.transform.LookAt(hit.point);
            bulletController.Target = hit.point;
            bulletController.TargetContactNormal = hit.normal;
            bulletController.Hit = true;
        }
        else
        {
            bulletController.Target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance; ;
            bulletController.Hit = false;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        var input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // ȸ��.
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        // �ִϸ��̼�
        if (move.sqrMagnitude > 0)
        {
            float forwardDegree = transform.forward.VectorToDegree();
            float moveDegree = move.VectorToDegree();
            float dirRadian = (moveDegree - forwardDegree + 90) * Mathf.PI / 180; //���Ȱ�
            Vector3 dir;
            dir.x = Mathf.Cos(dirRadian);// 
            dir.z = Mathf.Sin(dirRadian);//
            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", dir.z);
        }
        animator.SetFloat(parameterSpeed, move.sqrMagnitude);
        animator.SetBool(parameterIsMoving, move.sqrMagnitude > 0);
    }
    public float rotationSpeed = 5;
}

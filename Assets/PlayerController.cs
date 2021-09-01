using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public string parameterAttack = "StartFire";
    public string parameterSpeed = "Speed";
    public string parameterIsMoving = "IsMoving";
    public PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;
    InputAction aimAction;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    Transform cameraTransform;
    Animator animator;
    ProjectileParabolaDrawer projectileParabolaDrawer;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        projectileParabolaDrawer = GetComponentInChildren<ProjectileParabolaDrawer>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        shootAction.performed += ShootAction_performed;

        projectileParabolaDrawer.gameObject.SetActive(false);
        aimAction.performed += _ => projectileParabolaDrawer.gameObject.SetActive(true); // 궤적 보이게 하기
        aimAction.canceled += _ => projectileParabolaDrawer.gameObject.SetActive(false); // 궤적 감추

        projectileParabolaDrawer.Speed = bulletPrefab.GetComponent<IProjectile>().Speed;

        //아래처럼도 작성가능하지만 사용하지 않는 파라미터 굳이 적을 필요 없으니깐 위에서처럼 짧게 표현
        //aimAction.canceled += (InputAction.CallbackContext obj) => { projectileParabolaDrawer.gameObject.SetActive(false); }; 
    }


    public GameObject bulletPrefab;
    public GameObject barrelTransform;
    public Transform bulletParent;
    private float bulletHitMissDistance = 25f;

    public LayerMask bulletColllisionDetact = int.MaxValue;
    private void ShootAction_performed(InputAction.CallbackContext obj)
    {
        if (StageManager.GameState != GameStateType.Play)
            return;

        animator.SetTrigger(parameterAttack);

        GameObject projectile = bulletPrefab;
        if (nextSkillProjectile != null)
        {
            projectile = nextSkillProjectile;
            nextSkillProjectile = null;
        }
        GameObject bullet = Instantiate(projectile, barrelTransform.transform.position
            , Quaternion.LookRotation(cameraTransform.forward), bulletParent);

        var bulletController = bullet.GetComponent<IProjectile>();
        bulletController.CurrentAngle = projectileParabolaDrawer.currentAngle;
        projectileParabolaDrawer.Speed = bulletController.Speed;

        Vector3 rayStartPoint = ProjectileParabolaDrawer.GetRayStartPoint(cameraTransform, transform);
        if (Physics.Raycast(rayStartPoint, cameraTransform.forward
            , out RaycastHit hit, Mathf.Infinity, bulletColllisionDetact))
        {
            //print($"여기에 레이 충돌함, {hit.point}, {hit.point.z}");
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

        // 회전.
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        // 애니메이션
        if (move.sqrMagnitude > 0)
        {
            float forwardDegree = transform.forward.VectorToDegree();
            float moveDegree = move.VectorToDegree();
            float dirRadian = (moveDegree - forwardDegree + 90) * Mathf.PI / 180; //라디안값
            Vector3 dir;
            dir.x = Mathf.Cos(dirRadian);// 
            dir.z = Mathf.Sin(dirRadian);//
            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", dir.z);
        }
        animator.SetFloat(parameterSpeed, move.sqrMagnitude);
        animator.SetBool(parameterIsMoving, move.sqrMagnitude > 0);
    }

    public GameObject nextSkillProjectile;

    internal void UseSkill(SkillInfo skillInfo)
    {
        if (string.IsNullOrEmpty(skillInfo.arrowPrefabName) == false)
        {
            //skillInfo.arrowPrefabName 다음번 화살은 이것을 사용하자. 
            nextSkillProjectile = Resources.Load<GameObject>(skillInfo.arrowPrefabName);
        }
    }

    public float rotationSpeed = 5;
}

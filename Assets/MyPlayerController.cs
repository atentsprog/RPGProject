using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
public class MyPlayerController : MonoBehaviour
{
    public float _speed = 5;
    public Animator animator;
    public NavMeshAgent agent;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move.z = 1;
        if (Input.GetKey(KeyCode.S)) move.z = -1;   // 누름
        if (Input.GetKey(KeyCode.A)) move.x = -1;
        if (Input.GetKey(KeyCode.D)) move.x = 1;
        if (move != Vector3.zero)
        {
            Vector3 relateMove;
            relateMove = Camera.main.transform.forward * move.z; // 0, -1, 0
            relateMove += Camera.main.transform.right * move.x;
            relateMove.y = 0;
            move = relateMove;
            move.Normalize(); // z : -1, x : 0
            var pos = agent.nextPosition;
            pos += move * _speed * Time.deltaTime;
            agent.nextPosition = pos;

            float forwardDegree = transform.forward.VectorToDegree();
            float moveDegree = move.VectorToDegree();
            float dirRadian = (moveDegree - forwardDegree + 90) * Mathf.PI / 180; //라디안값
            Vector3 dir;
            dir.x = Mathf.Cos(dirRadian);// 
            dir.z = Mathf.Sin(dirRadian);//
            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", dir.z);
        }
        animator.SetFloat("Speed", move.sqrMagnitude);
    }
}
static public class MyExt
{ 
    public static float VectorToDegree(this Vector3 v)
    {
        float radian = Mathf.Atan2(v.z, v.x);
        return (radian * Mathf.Rad2Deg);
    }
}
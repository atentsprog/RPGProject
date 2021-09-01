using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    public float groundCheckLength = 0.1f;
    public LayerMask groundLayer;
    Ray ray;
    bool mIsGrounded;
    public bool isGrounded => mIsGrounded;

    RaycastHit hitInfo;
    private void Update()
    {
        ray = new Ray(transform.position, Vector3.down);
        Vector3 playerPosition = transform.position;

        Physics.Raycast(ray, out hitInfo, groundCheckLength, groundLayer);
        if(hitInfo.transform != null)
        { 
            mIsGrounded = hitInfo.transform != null;
            return;
        }

        // 땅 밑으로 파고 들었을대때에 대한 체크
        Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out hitInfo, 1, groundLayer);
        if (hitInfo.transform != null && playerPosition.y < hitInfo.point.y)
        {
            //print($"Player Y :{playerPosition.y}, hitY : {hitInfo.point.y}");
            transform.position = hitInfo.point;
            mIsGrounded = hitInfo.transform != null;
            return;
        }
        mIsGrounded = false;
    }

    internal void Move(Vector3 move)
    {
        transform.Translate(move, Space.World);
    }
}

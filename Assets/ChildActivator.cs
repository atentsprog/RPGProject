using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ϸ� �ڽ� ������Ʈ ��� Ȱ���� �Ѵ�
/// </summary>
public class ChildActivator : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}

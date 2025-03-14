using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private Vector3 initalPosition;
    public GameObject targetObject;
    public LayerMask targetLayer;

    void Start()
    {
        initalPosition = targetObject.transform.position;   // �ʱ� ��ġ ����
    }

    /// <summary>
    /// ��ư Ʈ���� �� ȣ��� �Լ�
    /// </summary>
    public void ResetObjectPosition()
    {
        targetObject.transform.position = initalPosition;
    }

    /// <summary>
    /// �÷��̾�� ����� �� �Լ� ȣ��
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (targetLayer == (targetLayer | (1<<other.gameObject.layer)))
        {
            ResetObjectPosition();
        }
    }
}

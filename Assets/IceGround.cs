using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private string playerTag = "Player";
    private PlayerController playerController;
    private void Start()
    {
        // �÷��̾� ��������
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // �÷��̾� ������ ����
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // �÷��̾� ������ ���� ����
        }
    }
}

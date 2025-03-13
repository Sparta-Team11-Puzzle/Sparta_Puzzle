using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private string playerTag = "Player";

    private Stage1 stage;
    private TestPlayer playerController;
    private Rigidbody playerRigidbody;

    public bool stayPlayer { get; private set; }

    public void InitObject(Stage1 stage)
    {
        this.stage = stage;

        // �÷��̾� ��������
        playerController = FindObjectOfType<TestPlayer>();
        playerRigidbody = playerController.GetComponent<Rigidbody>();

        stayPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = true;

            // �÷��̾� ������ ����
            playerController.canMove = false;
            // �÷��̾� �ӵ� �ʱ�ȭ
            playerRigidbody.velocity = Vector3.zero;

            Vector3 force = stage.GetDirection(collision.transform);
            playerRigidbody.AddForce(force * 10, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = false;

            // �÷��̾� ������ ���� ����
            playerController.canMove = true;

            // �� ����
            playerRigidbody.Sleep();
        }
    }
}


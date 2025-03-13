using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private string playerTag = "Player";

    private Stage1 stage;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody playerRigidbody;

    public bool stayPlayer { get; private set; }    // �÷��̾ IceGround�� �ӹ����ִ� ����

    public void InitObject(Stage1 stage)
    {
        this.stage = stage;

        // �÷��̾� ��������
        playerController = FindObjectOfType<PlayerController>();
        playerRigidbody = playerController.GetComponent<Rigidbody>();

        stayPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = true;      
            stage.isSlide = true;   

            // �÷��̾� ������ ����
            playerController.SetCanMove(false);

            // �÷��̾� �ӵ� �ʱ�ȭ
            playerRigidbody.Sleep();
            playerRigidbody.velocity = Vector3.zero;

            // ���� �������� AddForce
            Vector3 force = stage.GetDirection(collision.transform);
            stage.moveDirection = force;
            playerRigidbody.AddForce(force * 10, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = false;
            stage.isSlide = false;

            // �÷��̾� ������ ���� ����
            playerController.SetCanMove(true);

            // �� ����
            playerRigidbody.Sleep();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAction : MonoBehaviour
{
    [Header("Push Info")]
    [SerializeField] float pushPower = 10f;
    [SerializeField] private GameObject pushingObject;
    [SerializeField] private bool isPushing;
    [SerializeField] private float pushingDistance = 4f;

    void Update()
    {
        HandlePushing();
    }

    void HandlePushing()
    {
        if (pushingObject == null)
        {
            isPushing = false;
            return;
        }
        float distanceToObject = Vector3.Distance(transform.position, pushingObject.transform.position);

        // �� �� �ִ� �Ÿ� �ȿ� �־����
        if (distanceToObject > pushingDistance)
        {
            isPushing = false;
            return;
        }

        // �б� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            isPushing = true;
        }

        // �б� �ߴ�
        if (Input.GetKeyUp(KeyCode.W))
        {
            isPushing = false;
        }

        // ������Ʈ �̵�
        if (isPushing)
        {
            // �÷��̾�� ������Ʈ ���� ����
            Vector3 pushDirection = pushingObject.transform.position - transform.position;
            pushDirection.y = 0;        // y ���� ����
            pushDirection.Normalize();  // ���� ����ȭ

            float pushForce = pushPower / (distanceToObject * distanceToObject);
            pushingObject.transform.Translate(pushDirection * pushForce * Time.deltaTime, Space.World);
        }
    }

    // �� �� �ִ� ������Ʈ�� ���� ��
    private void OnTriggerEnter(Collider other)
    {
        // �� �� �ִ� ������Ʈ �±�
        if (other.CompareTag("Pushable"))
        {
            pushingObject = other.gameObject;
        }
    }

    // �� �� �ִ� ������Ʈ�� ������ ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            pushingObject = null;
        }
    }
}

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

        // 밀 수 있는 거리 안에 있어야함
        if (distanceToObject > pushingDistance)
        {
            isPushing = false;
            return;
        }

        // 밀기 감지
        if (Input.GetKeyDown(KeyCode.W))
        {
            isPushing = true;
        }

        // 밀기 중단
        if (Input.GetKeyUp(KeyCode.W))
        {
            isPushing = false;
        }

        // 오브젝트 이동
        if (isPushing)
        {
            // 플레이어와 오브젝트 사이 방향
            Vector3 pushDirection = pushingObject.transform.position - transform.position;
            pushDirection.y = 0;        // y 방향 무시
            pushDirection.Normalize();  // 벡터 정규화

            float pushForce = pushPower / (distanceToObject * distanceToObject);
            pushingObject.transform.Translate(pushDirection * pushForce * Time.deltaTime, Space.World);
        }
    }

    // 밀 수 있는 오브젝트와 접촉 시
    private void OnTriggerEnter(Collider other)
    {
        // 밀 수 있는 오브젝트 태그
        if (other.CompareTag("Pushable"))
        {
            pushingObject = other.gameObject;
        }
    }

    // 밀 수 있는 오브젝트와 미접촉 시
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            pushingObject = null;
        }
    }
}

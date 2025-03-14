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
        initalPosition = targetObject.transform.position;   // 초기 위치 저장
    }

    /// <summary>
    /// 버튼 트리거 시 호출될 함수
    /// </summary>
    public void ResetObjectPosition()
    {
        targetObject.transform.position = initalPosition;
    }

    /// <summary>
    /// 플레이어와 닿았을 때 함수 호출
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

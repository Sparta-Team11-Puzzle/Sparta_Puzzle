using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private string playerTag = "Player";
    private PlayerController playerController;
    private void Start()
    {
        // 플레이어 가져오기
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // 플레이어 움직임 제한
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // 플레이어 움직임 제한 해제
        }
    }
}

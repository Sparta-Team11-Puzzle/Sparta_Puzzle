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

        // 플레이어 가져오기
        playerController = FindObjectOfType<TestPlayer>();
        playerRigidbody = playerController.GetComponent<Rigidbody>();

        stayPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = true;

            // 플레이어 움직임 제한
            playerController.canMove = false;
            // 플레이어 속도 초기화
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

            // 플레이어 움직임 제한 해제
            playerController.canMove = true;

            // 힘 제거
            playerRigidbody.Sleep();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private string playerTag = "Player";

    private Stage1 stage;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody playerRigidbody;

    public bool stayPlayer { get; private set; }    // 플레이어가 IceGround에 머물고있는 상태

    public void InitObject(Stage1 stage)
    {
        this.stage = stage;

        // 플레이어 가져오기
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

            // 플레이어 움직임 제한
            playerController.SetCanMove(false);

            // 플레이어 속도 초기화
            playerRigidbody.Sleep();
            playerRigidbody.velocity = Vector3.zero;

            // 입장 방향으로 AddForce
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

            // 플레이어 움직임 제한 해제
            playerController.SetCanMove(true);

            // 힘 제거
            playerRigidbody.Sleep();
        }
    }
}


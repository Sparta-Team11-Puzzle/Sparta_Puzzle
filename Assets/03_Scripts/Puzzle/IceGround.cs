using DataDeclaration;
using UnityEngine;

public class IceGround : MonoBehaviour, IEventTrigger
{
    private string playerTag = "Player";

    private Stage1 stage;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;

    public bool stayPlayer { get; private set; }    // 플레이어가 IceGround에 머물고있는 상태

    [SerializeField] private bool canSlide;
    public void EventTrigger()
    {
        canSlide = false;
    }

    /// <summary>
    /// 오브젝트 초기화
    /// </summary>
    /// <param name="stage">스테이지 참조</param>
    public void InitObject(Stage1 stage, Transform player)
    {
        this.stage = stage;

        // 플레이어 가져오기
        playerController = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody>();

        stayPlayer = false;
        canSlide = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canSlide)
            return;

        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = true;      
            stage.isSlide = true;
            stage.ActionBinding(true);

            // 플레이어 움직임 제한
            playerController.SetCanMove(false);

            // 플레이어 속도 초기화
            playerRigidbody.Sleep();
            playerRigidbody.velocity = Vector3.zero;

            // 입장 방향으로 AddForce
            Vector3 force = stage.GetDirection();
            stage.moveDirection = force;
            playerRigidbody.AddForce(force * 10, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!canSlide)
            return;

        if (collision.gameObject.CompareTag(playerTag))
        {
            stayPlayer = false;
            stage.isSlide = false;
            stage.ActionBinding(false);

            // 플레이어 움직임 제한 해제
            playerController.SetCanMove(true);

            // 힘 제거
            playerRigidbody.Sleep();
        }
    }
}


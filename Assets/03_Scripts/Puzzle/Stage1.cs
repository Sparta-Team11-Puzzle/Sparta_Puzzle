using UnityEngine;

public class Stage1 : BaseRoom
{
    private (float angle, Vector3 direction)[] snapData =
        {
            (0, new Vector3(0, 0, 1)),    // 정면
            (90, new Vector3(1, 0, 0)),   // 오른쪽
            (180, new Vector3(0, 0, -1)), // 뒤쪽
            (-90, new Vector3(-1, 0, 0))  // 왼쪽
        };

    // ===== 핵심 로직 =====
    private RaycastHit hit; 
    [SerializeField] private float brakeDistance;           // 브레이크 탐지 거리
    [SerializeField] private Vector3 brakeDistaceOffset;    // 브레이크 Ray Offset
    [SerializeField] private LayerMask obstacleLayer;       // 브레이크 LayerMask

    // ===== Object =====
    [SerializeField] private IceGround iceGround;           // 미끄러지는 바닥

    // ===== Player =====
    private Rigidbody playerRigidbody;                      // 플레이어 RigidBody
    private InputHandler inputHandler;                      // 플레이어 InputHandler
    PlayerController controller;

    // ===== Get / Set =====
    public Vector3 moveDirection { get; set; }
    public bool isSlide { get; set; }

    public override void InitRoom(DungeonSystem system)
    {
        base.InitRoom(system);

        // player 캐싱
        playerRigidbody = player.GetComponent<Rigidbody>();
        inputHandler = player.GetComponent<InputHandler>();
        controller = player.GetComponent<PlayerController>();

        // 오브젝트 초기화
        iceGround.InitObject(this, player);
        isSlide = false;
    }

    public void Update()
    {
        // 플레이어가 iceGround 위에 없으면 return
        if (!iceGround.stayPlayer)
            return;

        // 미끄러지는중
        if(isSlide)
        {
            // 장애물과 충돌할때까지
            if (!CheckObstacle(player.transform, moveDirection))
                return;

            // 충돌시 플레이어 멈춤
            playerRigidbody.Sleep();
            isSlide = false;
        }
    }

    private void Silde()
    {
        // 슬라이딩 상태
        if (isSlide)
            return;

        // 움직일 방향
        moveDirection = GetDirection();

        // 장애물 확인
        if (CheckObstacle(player.transform, moveDirection))
            return;

        // 장애물이 없으면 AddForce로 미끄러지기
        playerRigidbody.AddForce(moveDirection * 10, ForceMode.Impulse);
        isSlide = true;
    }

    /// <summary>
    /// Silde 메서드 바인딩 함수
    /// </summary>
    /// <param name="state">메서드 바인딩 여부</param>
    public void ActionBinding(bool state)
    {
        if (state)
            inputHandler.UseTrigger += Silde;

        else
            inputHandler.UseTrigger -= Silde;
    }


    /// <summary>
    /// 이동방향 장애물 확인 함수
    /// </summary>
    /// <param name="target">장애물을 확인하는 주체</param>
    /// <param name="direction">이동방향</param>
    /// <returns></returns>
    public bool CheckObstacle(Transform target, Vector3 direction)
    {
        // Ray 시작점 오프셋
        Vector3[] rayOffsets =
        {
            Vector3.zero,
            new Vector3(direction.z, 0, direction.x) * 0.45f,
            new Vector3(-direction.z, 0, -direction.x) * 0.45f
        };

        foreach (Vector3 offset in rayOffsets)
        {
            // Ray 시작점
            Vector3 rayPosition = target.position + brakeDistaceOffset + offset;

            // Draw
            Debug.DrawRay(rayPosition, direction * brakeDistance, Color.red);

            // RayCast
            if (Physics.Raycast(rayPosition, direction, out hit, brakeDistance, obstacleLayer))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 가장 가까운 방향(4방향) 을 얻는 함수
    /// </summary>
    /// <returns>방향 값</returns>
    public Vector3 GetDirection()
    {
        // 플레이어(카메라)가 바라보는 방향을 기준으로 한 y값
        float angle = Mathf.Atan2(controller.Forward.x, controller.Forward.z) * Mathf.Rad2Deg;

        // 가장 가까운 각도 찾기
        float closestAngle = snapData[0].angle;
        Vector3 closestDirection = snapData[0].direction;
        float minDiff = Mathf.Infinity;

        foreach (var snap in snapData)
        {
            float diff = Mathf.Abs(Mathf.DeltaAngle(angle, snap.angle));
            if (diff < minDiff)
            {
                closestAngle = snap.angle;
                closestDirection = snap.direction;
                minDiff = diff;
            }
        }

        // 가장 가까운 방향 반환
        return closestDirection;
    }
}
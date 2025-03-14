using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

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

    // ===== Get / Set =====
    public Vector3 moveDirection { get; set; }
    public bool isSlide { get; set; }

    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);

        // player 캐싱
        player = CharacterManager.Instance.Player.transform;
        playerRigidbody = player.GetComponent<Rigidbody>();

        // 오브젝트 초기화
        iceGround.InitObject(this, player);
        isSlide = false;
    }

    public override void UpdateRoom()
    {
        // 플레이어가 iceGround 위에 없으면 return
        if (!iceGround.stayPlayer)
            return;

        // 미끄러지는중
        if(isSlide)
        {
            if (!CheckObstacle(player.transform, moveDirection))
                return;

            playerRigidbody.Sleep();
            isSlide = false;
        }

        // 미끄러지는중이 아닐때 R 입력
        else if(Input.GetKeyDown(KeyCode.R))
        {
            // 1. 방향구하기
            moveDirection = GetDirection(player.transform);
            // 2. 장애물 확인
            if (CheckObstacle(player.transform, moveDirection))
                return;

            // 3. 장애물이없다면 AddForce
            playerRigidbody.AddForce(moveDirection * 10, ForceMode.Impulse);
            isSlide = true;
        }
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
            new Vector3(direction.z, 0, direction.x) * 0.3f,
            new Vector3(-direction.z, 0, -direction.x) * 0.3f
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
    /// <param name="target">대상 오브젝트</param>
    /// <returns></returns>
    public Vector3 GetDirection(Transform target)
    {
        // target이 바라보는 방향의 Y축 회전값 ( -180 ~ 180 )
        float angle = Mathf.Atan2(target.forward.x, target.forward.z) * Mathf.Rad2Deg;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class Stage1 : BaseRoom
{
    private (float angle, Vector3 direction)[] snapData =
        {
            (0, new Vector3(0, 0, 1)),    // ����
            (90, new Vector3(1, 0, 0)),   // ������
            (180, new Vector3(0, 0, -1)), // ����
            (-90, new Vector3(-1, 0, 0))  // ����
        };

    [Header("Brake")]
    private RaycastHit hit;
    [SerializeField] private float brakeDistance;
    [SerializeField] private Vector3 brakeDistaceOffset;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Object")]
    [SerializeField] private IceGround iceGround;
    [SerializeField] private Rigidbody playerRigidbody;

    public Vector3 moveDirection { get; set; }
    public bool isSlide { get; set; }

    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);
        iceGround.InitObject(this);

        isSlide = false;
        playerRigidbody = FindObjectOfType<Rigidbody>();
        player = playerRigidbody.transform;
    }

    public override void UpdateRoom()
    {
        // �÷��̾ iceGround ���� ������
        if (!iceGround.stayPlayer)
            return;

        // �̲���������
        if(isSlide)
        {
            if (!CheckObstacle(player.transform, moveDirection))
                return;

            playerRigidbody.Sleep();
            isSlide = false;
        }

        // �̲����������� �ƴҶ� R �Է�
        else if(Input.GetKeyDown(KeyCode.R))
        {
            // 1. ���ⱸ�ϱ�
            moveDirection = GetDirection(player.transform);
            // 2. ��ֹ� Ȯ��
            if (CheckObstacle(player.transform, moveDirection))
                return;

            // 3. ��ֹ��̾��ٸ� AddForce
            playerRigidbody.AddForce(moveDirection * 10, ForceMode.Impulse);
            isSlide = true;
        }
    }

    /// <summary>
    /// �̵����� ��ֹ� Ȯ�� �Լ�
    /// </summary>
    /// <param name="target">��ֹ��� Ȯ���ϴ� ��ü</param>
    /// <param name="direction">�̵�����</param>
    /// <returns></returns>
    public bool CheckObstacle(Transform target, Vector3 direction)
    {
        // Ray ������ ������
        Vector3[] rayOffsets =
        {
            Vector3.zero,
            new Vector3(direction.z, 0, direction.x) * 0.3f,
            new Vector3(-direction.z, 0, -direction.x) * 0.3f
        };

        foreach (Vector3 offset in rayOffsets)
        {
            // Ray ������
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
    /// ���� ����� ����(4����) �� ��� �Լ�
    /// </summary>
    /// <param name="target">��� ������Ʈ</param>
    /// <returns></returns>
    public Vector3 GetDirection(Transform target)
    {
        // target�� �ٶ󺸴� ������ Y�� ȸ���� ( -180 ~ 180 )
        float angle = Mathf.Atan2(target.forward.x, target.forward.z) * Mathf.Rad2Deg;

        // ���� ����� ���� ã��
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

        // ���� ����� ���� ��ȯ
        return closestDirection;
    }
}
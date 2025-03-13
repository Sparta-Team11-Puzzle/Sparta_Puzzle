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
    private RaycastHit hit;
    [SerializeField] private float brakeDistance;
    public Vector3 moveDirection { get; set; }
    public bool isSlide { get; set; }

    [SerializeField] private IceGround iceGround;
    [SerializeField] private TestPlayer tplayer;

    private void Start()
    {
        InitRoom(null);
    }
    private void Update()
    {
        UpdateRoom();
    }

    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);
        iceGround.InitObject(this);

        isSlide = false;
    }

    public override void UpdateRoom()
    {
        if (!iceGround.stayPlayer)
            return;

        // 미끄러지는중
        if(isSlide)
        {
            if (!CheckObstacle(tplayer.transform, moveDirection))
                return;

            tplayer.GetComponent<Rigidbody>().Sleep();
            isSlide = false;
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            moveDirection = GetDirection(tplayer.transform);
            if (CheckObstacle(tplayer.transform, moveDirection))
                return;

            tplayer.GetComponent<Rigidbody>().AddForce(moveDirection * 10, ForceMode.Impulse);
            isSlide = true;
        }
    }

    public bool CheckObstacle(Transform target, Vector3 direction)
    {
        Debug.DrawRay(target.position, direction * brakeDistance, Color.green);
        if (Physics.Raycast(target.position, direction, out hit, brakeDistance))
            return true;

        return false;
    }

    public Vector3 GetDirection(Transform target)
    {
        float angle = Mathf.Atan2(target.forward.x, target.forward.z) * Mathf.Rad2Deg;

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

        return closestDirection;
    }
}
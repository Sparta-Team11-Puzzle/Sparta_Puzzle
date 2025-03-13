using System.Collections;
using System.Collections.Generic;
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
    }

    public override void UpdateRoom()
    {
        if (!iceGround.stayPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 force = GetDirection(tplayer.transform);
            tplayer.GetComponent<Rigidbody>().AddForce(force * 10, ForceMode.Impulse);
        }
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
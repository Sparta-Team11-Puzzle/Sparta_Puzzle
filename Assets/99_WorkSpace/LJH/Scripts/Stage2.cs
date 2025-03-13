using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : BaseRoom
{

    public MaterialChanger[] materialChanger;
    public void Start()
    {
        InitRoom(null);

       
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].ChangeMaterialTemporarily();
        }
        else
        {
            Debug.LogError("MaterialChanger is not assigned in Stage2!");
        }
    }
    public void Update()
    {
        UpdateRoom();
    }
    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);
    }

    public override void UpdateRoom()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어가 닿았는지 확인
        {
            GameObject platform = other.gameObject;  // 충돌한 오브젝트 가져오기

            if (platform.name == "Glass_Platform")  // Glass_Platform인지 확인
            {
                Destroy(platform);  // Glass_Platform 제거
            }
        }
    }
}

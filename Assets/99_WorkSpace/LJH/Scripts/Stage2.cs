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
        if (other.CompareTag("Player"))  // �÷��̾ ��Ҵ��� Ȯ��
        {
            GameObject platform = other.gameObject;  // �浹�� ������Ʈ ��������

            if (platform.name == "Glass_Platform")  // Glass_Platform���� Ȯ��
            {
                Destroy(platform);  // Glass_Platform ����
            }
        }
    }
}

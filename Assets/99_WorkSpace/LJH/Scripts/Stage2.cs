using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : BaseRoom
{
    [SerializeField] private ShowPlatform platformTrigger;

    public MaterialChanger[] materialChanger;
    public GameObject invisibleWall;

    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);

        // 이벤트 넘겨주기
        platformTrigger.InitObject(ShowPlotform, HidePlatform);
    }

    public override void UpdateRoom()
    {

    }

    private void ShowPlotform()
    {
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].ChangeMaterialTemporarily();
        }
    }

    private void HidePlatform()
    {
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].RestoreMaterial();
        }

        // 벽 삭제
        RemoveInvisibleWall();
    }

    private void RemoveInvisibleWall()
    {
        if (invisibleWall != null)
        {
            Destroy(invisibleWall);  // InvisibleWall 삭제
        }
    }
}

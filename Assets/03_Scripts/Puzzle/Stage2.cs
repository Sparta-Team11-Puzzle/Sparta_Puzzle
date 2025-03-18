using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : BaseRoom
{
    [SerializeField] private ShowPlatform platformTrigger;

    public MaterialChanger[] materialChanger;
    public GameObject invisibleWall;

    public override void InitRoom(DungeonSystem system)
    {
        base.InitRoom(system);

        // 이벤트 넘겨주기
        platformTrigger.InitObject(ShowPlotform, HidePlatform);
    }

    private void ShowPlotform()
    {
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].ChangeMaterialTemporarily();
        }

        // 벽 활성화
        ActivateInvisibleWall();
    }

    private void HidePlatform()
    {
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].RestoreMaterial();
        }

        // 벽 비활성화
        DeactivateInvisibleWall();
    }

   
    private void ActivateInvisibleWall()
    {
        if (invisibleWall != null)
        {
            invisibleWall.SetActive(true); // InvisibleWall 활성화
        }
    }

    private void DeactivateInvisibleWall()
    {
        if (invisibleWall != null)
        {
            invisibleWall.SetActive(false); // InvisibleWall 비활성화
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : BaseRoom
{

    public MaterialChanger[] materialChanger;

    public GameObject invisibleWall;
    public void Start()
    {
        InitRoom(null);

       
        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].ChangeMaterialTemporarily();
        }

        // 5초 후에 InvisibleWall 제거
        Invoke("RemoveInvisibleWall", 5f);

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



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            Debug.Log("게임오버");
            //Invoke("RestartGame", 1f);
        }

        
    }

    private void RemoveInvisibleWall()
    {
        if (invisibleWall != null)
        {
            Destroy(invisibleWall);  // InvisibleWall을 제거
        }
    }
    //private void RestartGame()
    //{
    //    // 현재 씬을 다시 로드하여 게임 재시작
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneManager.LoadScene(currentSceneName);
    //}

}

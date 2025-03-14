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

              
        // 5�� �Ŀ� InvisibleWall ����
        Invoke("RemoveInvisibleWall", 5f);

    }
    public void Update()
    {
        UpdateRoom();
    }
    public override void InitRoom(DungeonManager manager)
    {
        base.InitRoom(manager);

        if (materialChanger != null)
        {
            for (int i = 0; i < materialChanger.Length; i++)
                materialChanger[i].ChangeMaterialTemporarily();
        }

    }

    public override void UpdateRoom()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ Ʈ���ſ� ���� �� �ߵ�

            if (materialChanger != null)
            {
                for (int i = 0; i < materialChanger.Length; i++)
                    materialChanger[i].ChangeMaterialTemporarily();
            }
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            Debug.Log("���ӿ���");
            //Invoke("RestartGame", 1f);
        }

        
    }

    private void RemoveInvisibleWall()
    {
        if (invisibleWall != null)
        {
            Destroy(invisibleWall);  // InvisibleWall�� ����
        }
    }
    //private void RestartGame()
    //{
    //    // ���� ���� �ٽ� �ε��Ͽ� ���� �����
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneManager.LoadScene(currentSceneName);
    //}

}

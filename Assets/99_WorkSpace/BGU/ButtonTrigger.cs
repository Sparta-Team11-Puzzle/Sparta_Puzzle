using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public List<InvisibleTriggerObject> targaetObject;          // 활성화 시킬 오브젝트 리스트

    /// <summary>
    /// 오브젝트 보이게 하기
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (InvisibleTriggerObject obj in targaetObject)
            {
                obj.OnVisible();
            }
        }
    }

    /// <summary>
    /// 오브젝트 안보이게 하기
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (InvisibleTriggerObject obj in targaetObject)
            {
                obj.Invisible();
            }
        }
    }
}
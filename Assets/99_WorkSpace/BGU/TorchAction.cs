using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAction : MonoBehaviour
{
    public GameObject fireEffect;       // 불 효과 오브젝트
    public bool isLight = false;          // 불이 켜진 상태 확인
    public static int lightTorchCoiunt = 0;   // 켜진 횃불 개수

    void Start()
    {
        fireEffect.SetActive(false);        // 초기 상태 비활성화
    }

    public void ToggleFire()
    {
        // 불이 꺼진 상태
        if (!isLight)
        {
            fireEffect.SetActive(true);
            isLight = true;
            lightTorchCoiunt++;
        }
        else
        {
            fireEffect.SetActive(false);
            isLight = false;
            lightTorchCoiunt--;
        }

        // 켜진 횃불이 2개라면
        if (lightTorchCoiunt == 2)
        {
            // "Door" 이름의 오브젝트 비활성화
            GameObject.Find("Door").SetActive(false);
        }
    }
}

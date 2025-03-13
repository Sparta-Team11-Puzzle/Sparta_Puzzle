using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTriggerObject : MonoBehaviour
{
    private Renderer objectRenderer;            // 렌더러 컴포넌트
    private Color originalColor;                // 원래 색상

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;          // 원래 색상 저장

        Invisible();            // 시작시 투명
    }

    /// <summary>
    /// 오브젝트를 안보이게 만들기
    /// </summary>
    public void Invisible()
    {
        Color tempColor = originalColor;            // 원래 색상 복사
        tempColor.a = 0f;                           // 알파 값을 0으로 설정
        objectRenderer.material.color = tempColor;  // 변경된 색상 적용
    }

    /// <summary>
    /// 오브젝트를 보이게 만들기
    /// </summary>
    public void OnVisible()
    {
        objectRenderer.material.color = originalColor;      // 원래 색상으로 복원
    }
}

using DataDeclaration;
using UnityEngine;

/// <summary>
/// 메인 UI 상속 클래스
/// </summary>
public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;
    protected UIType uiType;

    /// <summary>
    /// UI를 UIManager 필드값에 할당 기능
    /// </summary>
    /// <param name="manager">UIManager</param>
    public virtual void Init(UIManager manager)
    {
        this.uiManager = manager;
        uiManager.UIList.Add(this);
    }
    
    /// <summary>
    /// UI 활성화
    /// </summary>
    /// <param name="type">활성화 하려는 UI 타입</param>
    public abstract void ActiveUI(UIType type);
}

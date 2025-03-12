using DataDeclaration;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;
    protected UIType uiType;

    public void Init(UIManager manager)
    {
        this.uiManager = manager;
        uiManager.UIList.Add(this);
    }
    
    public abstract void ActiveUI(UIType type);
}

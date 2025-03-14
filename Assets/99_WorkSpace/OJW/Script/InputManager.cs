using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private InputActionAsset playerInput;

    public void Test()
    {
        // 불러오기
        string loadDate = PlayerPrefs.GetString("bindingOverrides", "{}");
        playerInput.LoadBindingOverridesFromJson(loadDate);
        
        var jumpAction = playerInput.FindAction("Jump");
        jumpAction.ApplyBindingOverride("<Keyboard>/space");
        
        var moveAction = playerInput.FindAction("Move");
        
        if (moveAction != null)
        {
            int upIndex = moveAction.GetBindingIndex(null,"<Keyboard>/w");
            int downIndex = moveAction.GetBindingIndex(null,"<Keyboard>/s");
            int leftIndex = moveAction.GetBindingIndex(null,"<Keyboard>/a");
            int rightIndex = moveAction.GetBindingIndex(null,"<Keyboard>/d");
        
            moveAction.ApplyBindingOverride(upIndex, "<Keyboard>/upArrow");
            moveAction.ApplyBindingOverride(downIndex, "<Keyboard>/downArrow");
            moveAction.ApplyBindingOverride(leftIndex, "<Keyboard>/leftArrow");
            moveAction.ApplyBindingOverride(rightIndex, "<Keyboard>/rightArrow");
        }
        
        // 저장
        string saveData = playerInput.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("bindingOverrides", saveData);
    }
}

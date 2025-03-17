using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyBindingUI : MonoBehaviour
{
    [SerializeField] private KeyBindData bindData;
    private InputAction action;
    private int bindingIndex;
    
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    [SerializeField] private TextMeshProUGUI actionName;
    [SerializeField] private TextMeshProUGUI bindingButtonText;
    [SerializeField] private Button bindingButton;
    [SerializeField] private GameObject waitForInputKey;

    private void Awake()
    {
        action = bindData.actionReference.action;
        bindingIndex = action.GetBindingIndex();

        actionName.text = bindData.displayActionName;
        bindingButtonText.text = action.bindings[bindingIndex].isComposite
            ? action.GetBindingDisplayString((int)bindData.bindingIndex)
            : action.GetBindingDisplayString();
        
        bindingButton.onClick.AddListener(StartRebind);
    }

    private void StartRebind()
    {
        action.Disable();
        
        waitForInputKey.SetActive(true);
        bindingButtonText.text = string.Empty;
        
        rebindOperation = action.PerformInteractiveRebinding((int)bindData.bindingIndex)
            .WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => OnCompleteRebind()).Start();
    }

    private void OnCompleteRebind()
    {
        rebindOperation.Dispose();
        
        action.Enable();
        
        bindingButtonText.text = action.bindings[bindingIndex].isComposite
            ? action.GetBindingDisplayString((int)bindData.bindingIndex)
            : action.GetBindingDisplayString();
        
        waitForInputKey.SetActive(false);

        action.SaveBindingOverridesAsJson();
    }

    public void UpdateUI()
    {
        bindingButtonText.text = action.bindings[bindingIndex].isComposite
            ? action.GetBindingDisplayString((int)bindData.bindingIndex)
            : action.GetBindingDisplayString();
    }
}
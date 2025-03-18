using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 키 바인딩 클래스
/// </summary>
public class KeyBindingUI : MonoBehaviour
{
    [SerializeField] private KeyBindData bindData; // keyBind ScriptableObject
    private InputAction action;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation; // 키 할당용 객체

    [SerializeField] private TextMeshProUGUI actionName; // 표시될 이름
    [SerializeField] private TextMeshProUGUI bindingButtonText; // 할당된 키 표시
    [SerializeField] private Button bindingButton;
    [SerializeField] private GameObject waitForInputKey;

    private void Awake()
    {
        action = bindData.actionReference.action;

        actionName.text = bindData.displayActionName;
        bindingButtonText.text = action.GetBindingDisplayString((int)bindData.bindingIndex);
        bindingButton.onClick.AddListener(StartRebind);
    }

    /// <summary>
    /// 키 변경 기능
    /// </summary>
    private void StartRebind()
    {
        action.Disable();

        waitForInputKey.SetActive(true);
        bindingButtonText.text = string.Empty;

        rebindOperation = action.PerformInteractiveRebinding((int)bindData.bindingIndex)
            .WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => OnCompleteRebind())
            .Start();
    }

    /// <summary>
    /// 키 변경 완료 시 실행될 로직
    /// </summary>
    private void OnCompleteRebind()
    {
        rebindOperation.Dispose();

        action.Enable();

        bindingButtonText.text = action.GetBindingDisplayString((int)bindData.bindingIndex);

        waitForInputKey.SetActive(false);

        action.SaveBindingOverridesAsJson();
    }

    /// <summary>
    /// UI 갱신 메서드
    /// </summary>
    public void UpdateUI()
    {
        //TODO: 버그 수정은 됐지만 왜 됨???
        // bindingButtonText.text = action.bindings[bindingIndex].isComposite
        //     ? action.GetBindingDisplayString((int)bindData.bindingIndex)
        //     : action.GetBindingDisplayString();
        bindingButtonText.text = action.GetBindingDisplayString((int)bindData.bindingIndex);
    }
}
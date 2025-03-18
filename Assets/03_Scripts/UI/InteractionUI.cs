using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private KeyBindData bindData;
    private InputAction action;
    private int bindingIndex;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    [SerializeField] private TextMeshProUGUI bindingButtonText;

    private void Awake()
    {
        action = bindData.actionReference.action;
        bindingIndex = action.GetBindingIndex();

        bindingButtonText.text = action.GetBindingDisplayString((int)bindData.bindingIndex);
    }

    private void OnEnable()
    {
        bindingButtonText.text = action.GetBindingDisplayString((int)bindData.bindingIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindButton : MonoBehaviour
{
    private RebindingKeys rebindingKeys;
    [SerializeField] private InputActionReference mappedAction;
    [SerializeField] private TextMeshProUGUI bindingKeyName;
    [SerializeField] private GameObject ShowRebind;

    private void Start()
    {
        rebindingKeys = GetComponentInParent<RebindingKeys>();
        bindingKeyName.text = InputControlPath.ToHumanReadableString(
            mappedAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void OnRebindKey()
    {
        ShowRebind.SetActive(true);
        rebindingKeys.RebindKey(mappedAction);
        rebindingKeys.OnComplet += UpdateUI;
    }

    void UpdateUI()
    {
        ShowRebind.SetActive(false);

        bindingKeyName.text = InputControlPath.ToHumanReadableString(
            mappedAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingKeys.OnComplet -= UpdateUI;
    }
}

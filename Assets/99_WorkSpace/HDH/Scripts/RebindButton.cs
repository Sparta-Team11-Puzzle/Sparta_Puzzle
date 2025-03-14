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
    }

    public void OnRebindKey()
    {
        rebindingKeys.RebindKey(mappedAction);
    }
}

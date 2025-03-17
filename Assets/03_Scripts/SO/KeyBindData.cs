using UnityEngine;
using UnityEngine.InputSystem;

public enum BindingIndex
{
    None,
    Forward = 1,
    Backward,
    Left,
    Right
}

[CreateAssetMenu(fileName = "KeyBind", menuName = "ScriptableObjects/KeyBind")]
public class KeyBindData : ScriptableObject
{
    public InputActionReference actionReference;
    public string displayActionName;

    [Space] public bool isMoveKey;
    public BindingIndex bindingIndex;
}

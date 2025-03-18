using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 복합 키 구성인 이동키 인덱스 타입
/// </summary>
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
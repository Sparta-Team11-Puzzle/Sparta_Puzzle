using UnityEngine;

public class Player : MonoBehaviour
{
    // PlayerController 속성으로, 외부에서 읽기만 가능하고 내부에서는 설정 가능
    public PlayerController Controller { get; private set; }

    // PlayerData 속성으로, 외부에서 읽기만 가능하고 내부에서는 설정 가능
    public PlayerData Data { get; private set; }

    public InteractionHandler Interact { get; private set; }

    private void Awake()
    {
        // CharacterManager의 인스턴스를 가져와 Player 속성에 현재 객체를 할당
        CharacterManager.Instance.Player = this;

        // PlayerController 컴포넌트를 가져와 Controller 속성에 할당
        Controller = GetComponent<PlayerController>();

        // PlayerData 컴포넌트를 가져와 Data 속성에 할당
        Data = GetComponent<PlayerData>();

        Interact = GetComponent<InteractionHandler>();
    }
}

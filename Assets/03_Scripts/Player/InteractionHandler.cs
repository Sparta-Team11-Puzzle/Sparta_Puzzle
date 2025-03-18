using DataDeclaration;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer; //감지할 레이어
    [SerializeField] float interactRange; //감지 범위
    [SerializeField] Transform interactPoint; //ray가 시작될 지점

    [SerializeField] GameObject curInteraction; //현재 상호작용하고 있는 게임 오브젝트
    Camera camera; //카메라
    InputHandler inputHandler; //UseTrigger 접근을 위한 InputHandler 변수 지정
    [SerializeField] IInteractable interactable; //현재 상호작용하고 있는 게임 오브젝트의 IInteractable 인터페이스

    PlayerController controller;

    private void Start()
    {
        camera = Camera.main; // 메인 카메라를 가져와 camera 변수에 할당
        inputHandler = GetComponent<InputHandler>(); //컴포넌트를 변수에 할당
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        //플레이어가 바라보는 방향의 x,z축 성분과 카메라의 y축 성분을 이용해 바라보는 방향을 계산
        Vector3 rayDirection = new Vector3(controller.Forward.x, camera.transform.forward.y, controller.Forward.z);
        //ray가 시작되는 위치에서 바라보는 방향으로 ray를 발사
        Ray ray = new Ray(interactPoint.position, rayDirection);
        //충돌한 오브젝트 정보를 저장
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, targetLayer))
        {
            //상호작용 가능한 아이템을 표시하는 UI를 활성화
            UIManager.Instance.InGameUI.InteractGuide.SetActive(true);

            //기존에 저장된 오브젝트와 다른 오브젝트가 충돌하였을 때 갱신
            if (hit.collider.gameObject != curInteraction)
            {
                //현재 충돌 중인 오브젝트를 저장
                curInteraction = hit.collider.gameObject;

                InteractionReset();

                //해당 오브젝트에서 IInteractable을 가져옴
                if (curInteraction.TryGetComponent<IInteractable>(out interactable))
                {
                    //UseTrigger에 Interact 메서드를 구독
                    inputHandler.UseTrigger += interactable.Interact;
                }
            }
        }
        else
        {
            //UseTrigger에 구독된 Interact 메서드를 연결 취소
            InteractionReset();
            //변수를 초기화
            curInteraction = null;
            //상호작용 가능한 아이템을 표시하는 UI를 비활성화
            UIManager.Instance.InGameUI.InteractGuide.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (camera == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(interactPoint.position, interactPoint.position 
            + new Vector3(controller.Forward.x, camera.transform.forward.y, controller.Forward.z).normalized * interactRange);
    }

    public void InteractionReset()
    {
        if (interactable != null)
        {
            inputHandler.UseTrigger -= interactable.Interact;
            interactable = null;
        }
            
    }

}

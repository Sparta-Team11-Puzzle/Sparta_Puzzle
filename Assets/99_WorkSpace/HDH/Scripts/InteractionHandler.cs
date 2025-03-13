using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float interactRange;
    [SerializeField] Transform interactPoint;

    GameObject curInteraction;
    Camera camera;
    InputHandler inputHandler;

    private void Start()
    {
        camera = Camera.main;
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 rayDirection = new Vector3(transform.forward.x, camera.transform.forward.y, transform.forward.z);

        Ray ray = new Ray(interactPoint.position, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, targetLayer))
        {
            if (hit.collider.gameObject != curInteraction)
            {
                curInteraction = hit.collider.gameObject;

                //상호작용 처리
                //IInteractable interactable;

                //if(curInteraction.TryGetComponent<IInteractable>(out interactable))
                //{
                //    inputHandler.UseTrigger += interactable.Active;
                //}

            }
        }
        else
        {
            curInteraction = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (camera == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(interactPoint.position, interactPoint.position + new Vector3(transform.forward.x, camera.transform.forward.y, transform.forward.z).normalized * interactRange);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float interactRange;
    [SerializeField] Transform interactPoint;

    [SerializeField] GameObject curInteraction;
    Camera camera;
    InputHandler inputHandler;
    IInteractable interactable;

    PlayerController playerController;

    private void Start()
    {
        camera = Camera.main;
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 rayDirection = new Vector3(playerController.Forward.x, camera.transform.forward.y, playerController.Forward.z);

        Ray ray = new Ray(interactPoint.position, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, targetLayer))
        {
            if (hit.collider.gameObject != curInteraction)
            {
                curInteraction = hit.collider.gameObject;

                if (curInteraction.TryGetComponent<IInteractable>(out interactable))
                {
                    inputHandler.UseTrigger += interactable.Interact;
                }

            }
        }
        else
        {
            if (curInteraction != null && curInteraction.TryGetComponent<IInteractable>(out interactable))
            {
                inputHandler.UseTrigger -= interactable.Interact;
            }
            interactable = null;
            curInteraction = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (camera == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(interactPoint.position, interactPoint.position + new Vector3(playerController.Forward.x, camera.transform.forward.y, playerController.Forward.z).normalized * interactRange);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Active();
}

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

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (curInteraction == null)
                return;

            curInteraction.GetComponent<IInteractable>().Interact();
        }
    }

    void CheckObject()
    {
        Ray ray = new Ray(interactPoint.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, targetLayer))
        {
            if (hit.collider.gameObject != curInteraction)
            {
                curInteraction = hit.collider.gameObject;

                IInteractable interactable;

                if(curInteraction.TryGetComponent<IInteractable>(out interactable))
                {
                    inputHandler.UseTrigger += interactable.Active;
                }

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
        Gizmos.DrawLine(interactPoint.position, interactPoint.position + camera.transform.forward.normalized * interactRange);
    }

}

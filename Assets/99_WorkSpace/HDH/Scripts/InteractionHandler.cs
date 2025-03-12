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

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        CheckObject();
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

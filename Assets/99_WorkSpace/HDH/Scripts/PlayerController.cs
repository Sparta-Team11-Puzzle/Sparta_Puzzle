using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;

    [Header("Camera Info")]
    [SerializeField] float cameraSensitivity;
    private float camRotX;
    private float camRotY;
    private Camera camera;
    [SerializeField] private float maxRotX;
    [SerializeField] private float minRotX;
    [SerializeField] Transform fpsCameraTransform;


    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Look();
    }

    void Look()
    {
        float mouseX = inputHandler.mouseDelta.x * Time.deltaTime * cameraSensitivity;
        float mouseY = inputHandler.mouseDelta.y * Time.deltaTime * cameraSensitivity;

        camRotY += mouseX;
        camRotX -= mouseY;

        camRotX = Mathf.Clamp(camRotX, minRotX, maxRotX);

        camera.transform.position = fpsCameraTransform.position;
        camera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0);
        transform.rotation = Quaternion.Euler(0, camRotY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody rigidbody;
    private PlayerData playerData;

    [Header("Camera Info")]
    [SerializeField] float cameraSensitivity;
    private float camRotX;
    private float camRotY;
    private Camera camera;
    [SerializeField] private float maxRotX;
    [SerializeField] private float minRotX;
    [SerializeField] Transform fpsCameraTransform;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDrag;
    [SerializeField] float distanceToGround;
    bool isGround;


    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        rigidbody.freezeRotation = true;
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Update()
    {
        GroundCheck();
        ApplyDragForce();
    }

    private void FixedUpdate()
    {
        Move();
        LimitSpeed();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Look();
    }

    void GroundCheck()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanceToGround, groundLayer))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    void ApplyDragForce()
    {
        if (isGround)
            rigidbody.drag = groundDrag;
        else
            rigidbody.drag = 0;
    }

    void Move()
    {
        Vector3 moveDirection = transform.forward * inputHandler.movementInput.y + transform.right * inputHandler.movementInput.x;
        
        rigidbody.AddForce(moveDirection.normalized * playerData.Speed, ForceMode.Force);
    }

    void LimitSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if(horizontalVelocity.magnitude > playerData.Speed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * playerData.Speed;
            rigidbody.velocity = limitedVelocity;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.down * distanceToGround);
    }
}

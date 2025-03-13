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

    [Header("Move Info")]
    [SerializeField] private bool canMove;

    [Header("Jump Info")]
    [SerializeField] float jumpCooldown;
    private bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDrag;
    [SerializeField] float distanceToGround;
    [SerializeField] private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        rigidbody.freezeRotation = true;
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        inputHandler.OnJump += Jump;

    }

    private void Update()
    {
        GroundCheck();
        ApplyDragForce();
    }

    private void FixedUpdate()
    {
        Move(inputHandler.movementInput);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Look();
    }

    void GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanceToGround, groundLayer))
        {
            isGround = true;
            readyToJump = true;
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

    void Move( Vector2 movementInput )
    {
        if(!canMove || movementInput == Vector2.zero) return;

        Debug.Log(movementInput);

        Vector3 moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
        
        rigidbody.AddForce(moveDirection.normalized * playerData.Speed, ForceMode.Force);

        LimitSpeed();

    }

    void LimitSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if(horizontalVelocity.magnitude > playerData.Speed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * playerData.Speed;
            rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
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

        camera.transform.eulerAngles = new Vector3(camRotX, camRotY, 0);

        transform.eulerAngles = new Vector3(0, camRotY, 0);
    }

    void Jump()
    {
        if (readyToJump)
        {
            Debug.Log("Jump");
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

            rigidbody.AddForce(transform.up * playerData.JumpForce, ForceMode.Impulse);
            readyToJump = false;

            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);

        if(!readyToJump)
            readyToJump = true;
    }

    /// <summary>
    /// 플레이어의 이동 가능 상태를 변경
    /// </summary>
    /// <param name="value">이동 가능 상태 (true: 이동 가능, false: 이동 불가)</param>
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.down * distanceToGround);
    }
}

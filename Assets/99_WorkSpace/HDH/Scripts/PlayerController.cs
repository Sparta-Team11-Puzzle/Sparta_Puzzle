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
    
    [SerializeField]private float camRotX;
    [SerializeField]private float camRotY;
    
    private Camera camera;
    [SerializeField] private float maxRotX;
    [SerializeField] private float minRotX;
    [SerializeField] Transform fpsCameraTransform;

    [SerializeField] float TPSCameraDistance;

    [Header("Move Info")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool canMove;

    [Header("Jump Info")]
    [SerializeField] float jumpCooldown;
    private bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDrag;
    [SerializeField] float distanceToGround;
    [SerializeField] private bool isGround;

    [Header("Push Info")]
    [SerializeField] float pushSpeed;
    [SerializeField] private GameObject pushingObject;
    [SerializeField] private bool isPushing;

    /// <summary>
    /// �÷��̾��� ���� ������ ��ȯ
    /// </summary>
    public Vector3 Forward
    {
        get
        {
            float curDegree = camRotY;

            float x = Mathf.Sin(curDegree * Mathf.Deg2Rad);
            float z = Mathf.Cos(curDegree * Mathf.Deg2Rad);

            return new Vector3(x, 0, z).normalized;
        }
    }
    /// <summary>
    /// �÷��̾��� ������ 90�� ������ ��ȯ
    /// </summary>
    public Vector3 Right
    {
        get
        {
            float curDegree = camRotY - 90f;

            float x = Mathf.Sin(curDegree * Mathf.Deg2Rad);
            float z = Mathf.Cos(curDegree * Mathf.Deg2Rad);

            return new Vector3(x, 0, z).normalized;
        }
    }

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        rigidbody.freezeRotation = true;
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        inputHandler.JumpTrigger += Jump;

    }

    private void Update()
    {
        GroundCheck();
        ApplyDragForce();
        HandlePushing();
    }

    private void FixedUpdate()
    {
        Move(inputHandler.MovementInput);
    }

    void LateUpdate()
    {
        FPSLook();
        //TPSLook();
    }

    void GroundCheck()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

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

        Vector3 moveDirection = (Forward * movementInput.y + Right * -movementInput.x).normalized;

        rigidbody.AddForce(moveDirection * playerData.Speed, ForceMode.Force);

        LimitSpeed();

    }

    void Move2(Vector2 movementInput)
    {
        if (!canMove || movementInput == Vector2.zero) return;

        Vector3 moveDirection = (transform.forward * movementInput.y + transform.right * -movementInput.x).normalized;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        rigidbody.AddForce(transform.forward * playerData.Speed, ForceMode.Force);

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

    void FPSLook()
    {
        float mouseX = inputHandler.MouseDelta.x * Time.deltaTime * cameraSensitivity;
        float mouseY = inputHandler.MouseDelta.y * Time.deltaTime * cameraSensitivity;

        camRotY += mouseX;
        camRotX -= mouseY;

        camRotY %= 360f;

        camRotX = Mathf.Clamp(camRotX, minRotX, maxRotX);

        camera.transform.position = fpsCameraTransform.position;

        camera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0);

        transform.rotation = Quaternion.Euler(0, camRotY, 0);
    }

    /// <summary>
    /// 3��Ī ���� ���� ��...
    /// </summary>
    void TPSLook()
    {
        float mouseX = inputHandler.MouseDelta.x * Time.deltaTime * cameraSensitivity;
        float mouseY = inputHandler.MouseDelta.y * Time.deltaTime * cameraSensitivity;

        camRotY += mouseX;
        camRotX -= mouseY;

        SetPositionTPSCamera();
        RotateTPSCamera();
    }

    void SetPositionTPSCamera()
    {
        //���� ī�޶� �ٶ󺸴� ����� �ݴ��� ������ �ٶ�
        Vector2 tpsCameraDir = new Vector2(camRotY + 180, camRotX + 180);
        float tpsCameraPosX = Mathf.Sin(tpsCameraDir.x * Mathf.Deg2Rad);
        float tpsCameraPosZ = Mathf.Cos(tpsCameraDir.x * Mathf.Deg2Rad);
        float tpsCameraPosY = Mathf.Sin(tpsCameraDir.y * Mathf.Deg2Rad);

        Vector3 tpsCameraPos = new Vector3(tpsCameraPosX, tpsCameraPosY, tpsCameraPosZ) * TPSCameraDistance;

        camera.transform.position = fpsCameraTransform.position + tpsCameraPos;
    }

    void RotateTPSCamera()
    {
        Vector3 playerDir = fpsCameraTransform.position - camera.transform.position;

        playerDir.Normalize();

        //ī�޶��� y�� ȸ��
        float tpsCameraRotY = Mathf.Atan2(playerDir.x, playerDir.z) * Mathf.Rad2Deg;

        //ī�޶��� x�� ȸ��
        Vector2 horizontalDirection = new Vector2(playerDir.x, playerDir.z);

        float tpsCameraRotX = Mathf.Atan(playerDir.y / horizontalDirection.magnitude) * Mathf.Rad2Deg;

        camera.transform.eulerAngles = new Vector3(-tpsCameraRotX, tpsCameraRotY, 0f);

        //transform.rotation = Quaternion.Euler(0, tpsCameraRotY, 0);
        transform.forward = new Vector3(playerDir.x, 0, playerDir.z).normalized;

    }
    

    void Jump()
    {
        if (readyToJump)
        {
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
    /// �÷��̾��� �̵� ���� ���¸� ����
    /// </summary>
    /// <param name="value">�̵� ���� ���� (true: �̵� ����, false: �̵� �Ұ�)</param>
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f,
            transform.position + Vector3.up * 0.1f + Vector3.down * distanceToGround);
    }

    void HandlePushing()
    {
        // �б� ����
        if (Input.GetKeyDown(KeyCode.W) && pushingObject != null)
        {
            isPushing = true;
        }

        // �б� �ߴ�
        if (Input.GetKeyUp(KeyCode.W))
        {
            isPushing = false;
        }

        // ������Ʈ �̵�
        if (isPushing && pushingObject != null)
        {
            Vector3 pushDirection = Forward; // �÷��̾��� �� ����
            pushingObject.transform.Translate(pushDirection * pushSpeed * Time.deltaTime);
        }
    }

    // �� �� �ִ� ������Ʈ�� ���� ��
    private void OnTriggerEnter(Collider other)
    {
        // �� �� �ִ� ������Ʈ �±�
        if (other.CompareTag("Pushable"))
        {
            pushingObject = other.gameObject;
        }
    }

        // �� �� �ִ� ������Ʈ�� ������ ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pushsable"))
        {
            pushingObject = null;
        }
    }
}

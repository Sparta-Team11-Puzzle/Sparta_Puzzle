using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody rigidbody;
    private PlayerData playerData;

    // Camera Settings
    [Header("Camera Settings")]
    private float camRotX; // ī�޶� X ȸ��
    private float camRotY; // ī�޶� Y ȸ��
    private Camera camera; // ī�޶� ��ü
    [SerializeField] private bool FPSmode;
    [SerializeField] private Transform cameraTransform; // ī�޶� Ʈ������
    [SerializeField] private float cameraSensitivity; // ī�޶� ����
    [Header("Camera Settings-FPS")]
    [SerializeField] private float maxRotX; // �ִ� X ȸ�� ����
    [SerializeField] private float minRotX; // �ּ� X ȸ�� ����
    [Header("Camera Settings-TPS")]
    [SerializeField] private float minDistance; // �ּ� �Ÿ�
    [SerializeField] private float maxDistance; // �ִ� �Ÿ�
    [SerializeField] private float TPSCameraDistance; // 3��Ī ī�޶� �Ÿ�
    private const float ZOOM_RATIO = 0.1f; // �� ����

    // Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed; // ȸ�� �ӵ�
    [SerializeField] private bool canMove; // �̵� ���� ����

    // Jump Settings
    [Header("Jump Settings")]
    [SerializeField] private float jumpCooldown; // ���� ��Ÿ��
    private bool readyToJump; // ���� �غ� ����

    // Ground Check Settings
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer; // �ٴ� ���̾�
    [SerializeField] private float groundDrag; // �ٴڿ����� �巡��
    [SerializeField] private float distanceToGround; // �ٴڱ����� �Ÿ�
    [SerializeField] private bool isGround; // �ٴڿ� ��� �ִ��� ����

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
        inputHandler.CameraChangeTrigger += CameraChange;

    }

    private void Update()
    {
        GroundCheck();
        ApplyDragForce();
    }

    private void FixedUpdate()
    {
        if (FPSmode)
            FPSMove(inputHandler.MovementInput);
        else
            TPSMove(inputHandler.MovementInput);

    }

    void LateUpdate()
    {
        CameraZoom();

        if(FPSmode)
            FPSLook();
        else
            TPSLook();
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

    void FPSMove(Vector2 movementInput)
    {
        if (!canMove || movementInput == Vector2.zero) return;

        Vector3 moveDirection = (Forward * movementInput.y + Right * -movementInput.x).normalized;

        transform.forward = moveDirection;

        rigidbody.AddForce(transform.forward * playerData.Speed, ForceMode.Force);

        LimitSpeed();

    }

    void TPSMove(Vector2 movementInput)
    {
        if (!canMove || movementInput == Vector2.zero) return;

        Vector3 moveDirection = (Forward * movementInput.y + Right * -movementInput.x).normalized;

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

    void CameraChange()
    {
        FPSmode = !FPSmode;
    }

    void FPSLook()
    {
        camera.cullingMask &= ~LayerMask.GetMask("Player");

        float mouseX = inputHandler.MouseDelta.x * Time.deltaTime * cameraSensitivity;
        float mouseY = inputHandler.MouseDelta.y * Time.deltaTime * cameraSensitivity;

        camRotY += mouseX;
        camRotX -= mouseY;

        camRotY %= 360f;

        camRotX = Mathf.Clamp(camRotX, minRotX, maxRotX);

        camera.transform.position = cameraTransform.position;

        camera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0);

        transform.rotation = Quaternion.Euler(0, camRotY, 0);
    }

    void CameraZoom()
    {
        if (inputHandler.MouseZoom == 0) return;

        float mouseZoomDelta = inputHandler.MouseZoom > 0 ? -ZOOM_RATIO : ZOOM_RATIO; 

        TPSCameraDistance = Mathf.Clamp(TPSCameraDistance + mouseZoomDelta, minDistance, maxDistance);
    }

    /// <summary>
    /// 3��Ī ���� ���� ��...
    /// </summary>
    void TPSLook()
    {
        camera.cullingMask = -1;

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

        camera.transform.position = cameraTransform.position + tpsCameraPos;
    }

    void RotateTPSCamera()
    {
        Vector3 playerDir = cameraTransform.position - camera.transform.position;

        playerDir.Normalize();

        //ī�޶��� y�� ȸ��
        float tpsCameraRotY = Mathf.Atan2(playerDir.x, playerDir.z) * Mathf.Rad2Deg;

        //ī�޶��� x�� ȸ��
        Vector2 horizontalDirection = new Vector2(playerDir.x, playerDir.z);

        float tpsCameraRotX = Mathf.Atan(playerDir.y / horizontalDirection.magnitude) * Mathf.Rad2Deg;

        camera.transform.eulerAngles = new Vector3(-tpsCameraRotX, tpsCameraRotY, 0f);

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
}

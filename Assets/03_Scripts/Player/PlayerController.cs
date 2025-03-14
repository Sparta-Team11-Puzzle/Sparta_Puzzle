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
    private float camRotX; // 카메라 X 회전
    private float camRotY; // 카메라 Y 회전
    private Camera camera; // 카메라 객체
    [SerializeField] private bool FPSmode;
    [SerializeField] private Transform cameraTransform; // 카메라 트랜스폼
    [SerializeField] private float cameraSensitivity; // 카메라 감도
    [Header("Camera Settings-FPS")]
    [SerializeField] private float maxRotX; // 최대 X 회전 각도
    [SerializeField] private float minRotX; // 최소 X 회전 각도
    [Header("Camera Settings-TPS")]
    [SerializeField] private float minDistance; // 최소 거리
    [SerializeField] private float maxDistance; // 최대 거리
    [SerializeField] private float TPSCameraDistance; // 3인칭 카메라 거리
    private const float ZOOM_RATIO = 0.1f; // 줌 비율

    // Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed; // 회전 속도
    [SerializeField] private bool canMove; // 이동 가능 여부

    // Jump Settings
    [Header("Jump Settings")]
    [SerializeField] private float jumpCooldown; // 점프 쿨타임
    private bool readyToJump; // 점프 준비 상태

    // Ground Check Settings
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    [SerializeField] private float groundDrag; // 바닥에서의 드래그
    [SerializeField] private float distanceToGround; // 바닥까지의 거리
    [SerializeField] private bool isGround; // 바닥에 닿아 있는지 여부

    /// <summary>
    /// 플레이어의 정면 방향을 반환
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
    /// 플레이어의 오른쪽 90도 방향을 반환
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
    /// 3인칭 시점 개발 중...
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
        //현재 카메라가 바라보는 방향과 반대인 각도를 바라봄
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

        //카메라의 y축 회전
        float tpsCameraRotY = Mathf.Atan2(playerDir.x, playerDir.z) * Mathf.Rad2Deg;

        //카메라의 x축 회전
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
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f,
            transform.position + Vector3.up * 0.1f + Vector3.down * distanceToGround);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private bool FPSMode;
    [SerializeField] private bool mouseInputFlipX;
    [SerializeField] private bool mouseInputFlipY;
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
    [SerializeField] private bool canMove; // 이동 가능 여부

    // Jump Settings
    [Header("Jump Settings")]
    [SerializeField] private float jumpCooldown; // 점프 쿨타임
    [SerializeField] private bool readyToJump; // 점프 준비 상태

    // Ground Check Settings
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    [SerializeField] private float groundRayDistance;
    [SerializeField] private float distanceToGround; // 바닥까지의 거리
    [SerializeField] private bool isGround; // 바닥에 닿아 있는지 여부

    private bool startJump;

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
        inputHandler.CameraChangeTrigger += CameraChange;
    }

    private void Update()
    {
        isGround = GroundCheck();

        startJump = inputHandler.InputJump;
    }

    private void FixedUpdate()
    {
        if (!canMove || !isGround) return;
        Jump();
        Move(inputHandler.MovementInput);
    }

    void LateUpdate()
    {
        //마우스 입력 반전을 적용
        int mouseFlipX = mouseInputFlipX ? -1 : 1;
        int mouseFlipY = mouseInputFlipY ? -1 : 1;
        //감도를 고려한 마우스 입력 값
        float mouseX = inputHandler.MouseDelta.x * Time.deltaTime * cameraSensitivity * mouseFlipX;
        float mouseY = inputHandler.MouseDelta.y * Time.deltaTime * cameraSensitivity * mouseFlipY;
        //이를 회전 값에 적용
        camRotY += mouseX;
        camRotX -= mouseY;
        //값이 360도 이내에 있도록 함
        camRotY %= 360f;

        if (FPSMode)
            FPSLook();
        else
        {
            CameraZoom();
            TPSLook();
        }
            
    }

    bool GroundCheck()
    {
        //transform을 기준으로 4 지점에서 ray를 발사
        Ray[] rays = new Ray[4]
            {
            new Ray(transform.position + (transform.forward * groundRayDistance) + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position + (-transform.forward * groundRayDistance) + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position + (-transform.right * groundRayDistance) + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position + (transform.right * groundRayDistance) + Vector3.up * 0.1f, Vector3.down)
            };

        RaycastHit hit;

        //4개 중에 1개라도 true면 true를 반환
        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, out hit, distanceToGround, groundLayer))
            {
               return true;
            }
        }
        //그렇지 않을 경우 false를 반환
        return false;
    }

    void Move(Vector2 movementInput)
    {
        //입력이 없으면 속도를 0이 되도록 함
        if (movementInput == Vector2.zero)
        {
            rigidbody.velocity = Vector3.zero;
        }
        else
        {
            //마우스 입력에 따라 변경된 카메라 회전 값에 따라 Forward와 Right를 구한 방향으로 이동 방향을 구함
            Vector3 moveDirection = (transform.forward * movementInput.y + transform.right * -movementInput.x).normalized;
            //해당 방향으로 이동
            rigidbody.velocity = moveDirection * playerData.Speed;
        }
    }

    //카메라 모드를 전환
    void CameraChange()
    {
        FPSMode = !FPSMode;
    }
    //FPS 카메라의 움직임을 제어
    void FPSLook()
    {
        //Player 레이어를 보이지 않도록 함
        camera.cullingMask &= ~LayerMask.GetMask("Player");

        //x축 회전은 일정 각도를 제한하여 적용
        camRotX = Mathf.Clamp(camRotX, minRotX, maxRotX);

        //카메라의 위치를 플레이어 내부에 지정된 카메라 위치로 할당
        camera.transform.position = cameraTransform.position;
        //카메라 회전값은 앞서 구한 값을 적용
        camera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0);
        //앞서 구한 값을 플레이어의 y축 회전에도 적용(카메라가 플레이어가 동일하게 회전)
        transform.eulerAngles = new Vector3(0, camRotY, 0);
    }

    void CameraZoom()
    {
        //TPS 카메라의 거리를 조절
        if (inputHandler.MouseZoom == 0) return;
        //정해진 거리 조절 단위 변수 크기에 따라 바뀌도록 함
        float mouseZoomDelta = inputHandler.MouseZoom > 0 ? -ZOOM_RATIO : ZOOM_RATIO;
        //거리를 최대, 최소 범위 내에서 조절하도록 함
        TPSCameraDistance = Mathf.Clamp(TPSCameraDistance + mouseZoomDelta, minDistance, maxDistance);
    }

    void TPSLook()
    {
        //모든 레이어를 볼 수 있도록 함
        camera.cullingMask = -1;
        //x축 회전은 일정 각도를 제한하여 적용
        camRotX = Mathf.Clamp(camRotX, -100, 100);
        //TPS 카메라의 위치를 계산
        SetPositionTPSCamera();
        RotateTPSCamera();
    }

    void SetPositionTPSCamera()
    {
        //현재 카메라가 바라보는 방향과 반대 방향을 바라봄
        Vector2 tpsCameraDir = new Vector2(camRotY + 180, camRotX + 180);
        //해당 각도를 기준으로 카메라가 위치할 방향벡터를 구함
        float tpsCameraPosX = Mathf.Sin(tpsCameraDir.x * Mathf.Deg2Rad);
        float tpsCameraPosZ = Mathf.Cos(tpsCameraDir.x * Mathf.Deg2Rad);
        float tpsCameraPosY = Mathf.Sin(tpsCameraDir.y * Mathf.Deg2Rad);
        //크기가 1인 방향벡터에 지정된 거리만큼 곱하여 카메라가 위치할 지점을 구함
        Vector3 tpsCameraPos = new Vector3(tpsCameraPosX, tpsCameraPosY, tpsCameraPosZ).normalized * TPSCameraDistance;
        //이를 카메라 위치를 기준으로 계산하여 최종적인 위치를 지정
        camera.transform.position = cameraTransform.position + tpsCameraPos;
    }

    void RotateTPSCamera()
    {
        //플레이어로 향하는 방향벡터를 구함
        Vector3 playerDir = cameraTransform.position - camera.transform.position;
        //해당 방향 벡터를 노멀라이즈하여 방향 성분만 남김
        playerDir.Normalize();

        //카메라의 y축 회전
        //x축과 z축 값이 이루는 비율을 역탄젠트를 이용해 y축으로 회전해야하는 각도를 구함
        float tpsCameraRotY = Mathf.Atan2(playerDir.x, playerDir.z) * Mathf.Rad2Deg;

        //카메라의 x축 회전
        //x축 회전을 구하기 위해 x축과 z축으로 만들어지는 빗변의 길이를 구함 = x,z 평면 상에서 카메라와 플레이어 간의 거리
        Vector2 horizontalDirection = new Vector2(playerDir.x, playerDir.z);
        //거리의 크기와 y축 크기를 이용해 다시 역탄젠트로 각도를 구함
        float tpsCameraRotX = Mathf.Atan(playerDir.y / horizontalDirection.magnitude) * Mathf.Rad2Deg;

        //구한 각도를 카메라 회전에 적용
        camera.transform.rotation = Quaternion.Euler(-tpsCameraRotX, tpsCameraRotY, 0f);
        transform.eulerAngles = new Vector3(0, tpsCameraRotY, 0f);

    }

    void Jump()
    {
        //지면에 있으면서 startJump 입력이 있을 때 점프를 실행
        if (startJump && isGround)
        {
            if(!readyToJump)
            {
                OnJump();
            }
            else
            {
                readyToJump = false;
                Invoke("ResetJump", jumpCooldown);
            }

        }

    }
    
    void OnJump()
    {
        rigidbody.AddForce(transform.up * playerData.JumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
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

    /// <summary>
    /// 마우스 입력 반전을 관리
    /// </summary>
    /// <param name="axis">변경하려는 축 (0 : x축 , 1 : y축)</param>
    /// <param name="value">이동 가능 상태 (true: 마우스 입력 반전, false: 마우스 입력 반전하지 않음)</param>
    public void SetMouseInputFlip(int axis, bool value)
    {
        switch(axis)
        {
            case 0:
                mouseInputFlipX = value;
                break;
            case 1:
                mouseInputFlipY = value;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (transform.forward * groundRayDistance),
            transform.position + (transform.forward * groundRayDistance) + Vector3.down * distanceToGround);
        Gizmos.DrawLine(transform.position + (-transform.forward * groundRayDistance),
            transform.position + (-transform.forward * groundRayDistance) + Vector3.down * distanceToGround);
        Gizmos.DrawLine(transform.position + (-transform.right * groundRayDistance),
            transform.position + (-transform.right * groundRayDistance) + Vector3.down * distanceToGround);
        Gizmos.DrawLine(transform.position + (transform.right * groundRayDistance),
            transform.position + (transform.right * groundRayDistance) + Vector3.down * distanceToGround);
    }

}

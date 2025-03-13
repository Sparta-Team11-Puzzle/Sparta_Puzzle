using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody rb;
    private float camRotX;
    private float camRotY;
    [SerializeField] private float maxRotX;
    [SerializeField] private float minRotX;
    [SerializeField] float cameraSensitivity;

    public bool canMove;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    void Update()
    {
        Look();

        if(canMove)
            Move();
    }

    void Look()
    {
        float _mouseX = Input.GetAxisRaw("Mouse X");

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSensitivity;


        camRotY += mouseX;

        camRotX = Mathf.Clamp(camRotX, minRotX, maxRotX);

        transform.rotation = Quaternion.Euler(0, camRotY, 0);
    }

    void Move()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float right = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = transform.forward * forward + transform.right * right;

        rb.AddForce(moveDirection.normalized * 10, ForceMode.Force);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 정지
        rb.velocity = Vector3.zero;
    }
}

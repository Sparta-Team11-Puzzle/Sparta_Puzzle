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
        // �浹 �� ����
        rb.velocity = Vector3.zero;
    }
}

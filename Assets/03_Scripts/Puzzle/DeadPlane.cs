using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlane : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private PlayerSFXController controller;

    private void Start()
    {
        controller = FindObjectOfType<PlayerSFXController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            controller.PlayDieSound();
        }
    }
}
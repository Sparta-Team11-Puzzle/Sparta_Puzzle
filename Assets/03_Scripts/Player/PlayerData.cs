using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float Health {get; private set; }
    public float Speed { get; private set; }
    public float JumpForce { get; private set; }

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        Speed = speed;
        JumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

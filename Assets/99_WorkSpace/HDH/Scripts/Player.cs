using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerData Data { get; private set; }

    private void Start()
    {
        Controller = GetComponent<PlayerController>();
        Data = GetComponent<PlayerData>();
    }

}

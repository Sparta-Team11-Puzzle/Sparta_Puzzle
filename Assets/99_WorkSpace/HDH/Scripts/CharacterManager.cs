using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Player[] Players;

    // Start is called before the first frame update
    void Start()
    {
        Players = FindObjectsOfType<Player>();
    }
}

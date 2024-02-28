using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get 
        {
            if (instance == null) Debug.Log("Game Manager: 404");
            return instance; 
        } 
    }

    public bool hasKeysToCastle {get; set;}

    private void Awake()
    {
        instance = this;
    }
}

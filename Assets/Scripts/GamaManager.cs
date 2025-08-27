using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public InputManager inputManager;
    public BallManager ballManager;
    public SAOBallDatabase ballDatabase;

    private void Awake()
    {
        instance = this;
    }
}

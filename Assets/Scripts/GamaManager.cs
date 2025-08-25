using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager instance;
    
    public InputManager inputManager;
    public BallManager ballManager;
    public SAOBallDatabase ballDatabase;

    private void Awake()
    {
        instance = this;
    }
}

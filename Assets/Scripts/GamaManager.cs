using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public InputManager inputManager;
    public BallManager ballManager;
    public SAODatabase database;
    public ScoreManager scoreManager;
    public SoundManager soundManager;
    public MenuManager menuManager;
    public SaveManager saveManager;

    public bool isGameOver = false;

    public float saveBgmValue;
    public float saveSfxValue;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (isGameOver)
        {
            isGameOver = false;
            inputManager.enabled = false;
            menuManager.OnGameOver();
            List<Ball> ballList = ballManager.ballList;
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].transform.TryGetComponent(out Rigidbody2D rb);
                ballList[i].transform.TryGetComponent(out CircleCollider2D col);
                col.enabled = false;
                rb.bodyType = RigidbodyType2D.Kinematic;
                ballList[i].gameObject.transform.TryGetComponent(out Shake shake);
                shake.isShake = true;
                shake.enabled = true;
            }
        }
    }
}

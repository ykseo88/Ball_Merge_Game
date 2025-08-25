using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallData ballData = new BallData();
    public BallManager ballManager;
    public float defaultSize = 0.1f;
    private bool isFirstRand = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetDataByBallData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDataByBallData()
    {
        float size = ballData.Size * defaultSize;
        transform.localScale = new Vector3(size, size, size);
        transform.TryGetComponent(out SpriteRenderer spriteRenderer);
        spriteRenderer.sprite = ballData.image;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isFirstRand)
        {
            if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("Box"))
            {
                ballManager.isCurrentBallRand = true;
                isFirstRand = true;
                gameObject.layer = LayerMask.NameToLayer("Ball");
            }
        }
        
        if (other.transform.TryGetComponent(out Ball mergeBall))
        {
            if (mergeBall.ballData != null && mergeBall.ballData.mergeable && mergeBall.ballData.level == ballData.level)
            {
                
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallData ballData = new BallData();
    public BallManager ballManager;
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
        transform.localScale *= ballData.Size;
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
    }
}

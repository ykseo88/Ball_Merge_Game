using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallData ballData = new BallData();
    public BallManager ballManager;
    public float defaultSize;
    public bool isFirstRand = false;
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetDataByBallData();
        transform.TryGetComponent(out rb);
        transform.TryGetComponent(out coll);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetDataByBallData()
    {
        float size = ballData.Size * defaultSize;
        transform.localScale = new Vector3(size, size, size);
        transform.TryGetComponent(out SpriteRenderer spriteRenderer);
        spriteRenderer.sprite = ballData.image;
    }

    public void ResetBall()
    {
        isFirstRand = false;
    }

    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.isTrigger = false;
        transform.SetParent(null);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isFirstRand)
        {
            if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("Box"))
            {
                ballManager.SpawnBall();
                isFirstRand = true;
                gameObject.layer = LayerMask.NameToLayer("Ball");
            }
        }

        if (other.gameObject.CompareTag("GameOver"))
        {
            GameManager.instance.isGameOver = true;
            coll.enabled = false;
        }
        
        if (other.transform.TryGetComponent(out Ball mergeBall))
        {
            if (mergeBall.ballData != null && mergeBall.ballData.mergeable && mergeBall.ballData.level == ballData.level)
            {
                ballManager.MergeBall(ballData.level, other.contacts[0].point);
                ballManager.ballList.Remove(this);
                Destroy(gameObject);
            }
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    public InputManager inputManager;
    public GameObject ballPrefab;
    public Ball currentBall;
    [SerializeField]private int maxBallIndex;
    public bool isCurrentBallRand = false;
    private LineRenderer lineRenderer;
    private Vector2 randViewPoint = Vector2.zero;
    private int nonDetectlayerMask;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Image nextBall;
    private int nextBallIndex = -10;
    
    public Dictionary<int, int> mergeDic = new Dictionary<int, int>(); 
    public List<Ball> ballList = new List<Ball>();
    [SerializeField] private Transform maxTouchHeight;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.instance.ballManager = this;
        nonDetectlayerMask = LayerMask.GetMask("NonDetect");
        inputManager = GameManager.instance.inputManager;
        SetLineRender();
        SpawnBall();
        SetMergeDic();

    }

    private void Update()
    {
        UpdatePosition();
        UpdateLine();
    }

    private void SetMergeDic()
    {
        for (int i = 0; i < GameManager.instance.ballDatabase.ballDatas.Length; i++)
        {
            mergeDic.Add(i+1, 0);
        }
    }

    private void SetLineRender()
    {
        transform.TryGetComponent(out lineRenderer);
        lineRenderer.positionCount = 2;
    }

    private void UpdateLine()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 10f, ~nonDetectlayerMask);
        if (hit.collider != null)
        {
            randViewPoint = hit.point;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }

        if (randViewPoint != Vector2.zero)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, randViewPoint);
        }
    }

    private void UpdatePosition()
    {
        if(inputManager == null) inputManager = GameManager.instance.inputManager;
        if (inputManager._touchPosition.x >= leftLimit.position.x &&
            inputManager._touchPosition.x <= rightLimit.position.x &&
            inputManager._touchPosition.y < maxTouchHeight.position.y)
        {
            transform.position = new Vector3(inputManager._touchPosition.x, transform.position.y, 0);
        }
        
    }

    public void SpawnBall()
    {
        lineRenderer.enabled = true;
        GameObject temp = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        
        temp.transform.SetParent(transform);
        temp.transform.TryGetComponent(out currentBall);
        currentBall.transform.TryGetComponent(out Rigidbody2D rb);
        currentBall.transform.TryGetComponent(out CircleCollider2D col);
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.isTrigger = true;
        currentBall.ResetBall();
        SetRandomBall(currentBall);
        currentBall.SetDataByBallData();
        currentBall.ballManager = this;
        
    }

    public void DropBall()
    {
        if (GameManager.instance.inputManager._touchPosition.y < maxTouchHeight.position.y)
        {
            lineRenderer.enabled = false;
            ballList.Add(currentBall);
            currentBall.Drop();
        }
    }

    private void SetRandomBall(Ball ball)
    {
        if (nextBallIndex < 0) nextBallIndex = Random.Range(1, maxBallIndex + 1);
        BallData tempBallData = GameManager.instance.ballDatabase.GetBallDataByLevel(nextBallIndex);
        nextBallIndex = Random.Range(1, maxBallIndex + 1);
        nextBall.sprite = GameManager.instance.ballDatabase.GetBallDataByLevel(nextBallIndex).image;
        ball.ballData = tempBallData;
    }

    public void MergeBall(int level, Vector3 hitPosion)
    {
        mergeDic[level] += 1;
        if (mergeDic[level] >= 2)
        {
            mergeDic[level] = 0;
            GameObject temp = Instantiate(ballPrefab, hitPosion, Quaternion.identity);
            temp.transform.TryGetComponent(out Ball newBall);
            ballList.Add(newBall);
            newBall.gameObject.layer = LayerMask.NameToLayer("Ball");
            newBall.ballManager = this;
            newBall.transform.TryGetComponent(out Rigidbody2D rb);
            newBall.transform.TryGetComponent(out CircleCollider2D col);
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.isTrigger = false;
            newBall.ballData = GameManager.instance.ballDatabase.GetBallDataByLevel(level + 1);
            newBall.SetDataByBallData();
            GameManager.instance.scoreManager.currentScore += newBall.ballData.score;
            GameManager.instance.scoreManager.UpdateScore();
        }
    }
}

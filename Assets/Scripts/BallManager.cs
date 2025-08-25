using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        
    }

    private void Start()
    {
        GamaManager.instance.ballManager = this;
        nonDetectlayerMask = LayerMask.GetMask("NonDetect");
        inputManager = GamaManager.instance.inputManager;
        SetLineRender();
        SpawnBall();
        
    }

    private void Update()
    {
        UpdatePosition();
        CheckCurrentBallRand();
        UpdateLine();
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
        if(inputManager == null) inputManager = GamaManager.instance.inputManager;
        transform.position = new Vector3(inputManager._touchPosition.x, transform.position.y, 0);
    }

    public void SpawnBall()
    {
        
        lineRenderer.enabled = true;
        GameObject temp = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        
        temp.transform.SetParent(transform);
        temp.transform.TryGetComponent(out currentBall);
        SetRandomBall(currentBall);
        currentBall.ballManager = this;
    }

    public void DropBall()
    {
        lineRenderer.enabled = false;
        currentBall.transform.TryGetComponent(out CircleCollider2D collider);
        currentBall.transform.TryGetComponent(out Rigidbody2D rigidbody);
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.isTrigger = false;
        currentBall.transform.SetParent(null);
    }

    private void CheckCurrentBallRand()
    {
        if (isCurrentBallRand)
        {
            SpawnBall();
            isCurrentBallRand = false;
        }
    }

    private void SetRandomBall(Ball ball)
    {
        int randInt = Random.Range(1, maxBallIndex + 1);
        
        BallData tempBallData = GamaManager.instance.ballDatabase.GetBallDataByLevel(randInt);
        
        ball.ballData.name = tempBallData.name;
        ball.ballData.level = tempBallData.level;
        ball.ballData.mergeable = tempBallData.mergeable;
        ball.ballData.image = tempBallData.image;
        ball.ballData.Size = tempBallData.Size;
    }
}

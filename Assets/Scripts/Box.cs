using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Transform[] lineIndexs = new Transform[4];
    private LineRenderer lineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLineRenderer()
    {
        transform.TryGetComponent(out lineRenderer);
        lineRenderer.positionCount = lineIndexs.Length;
        for (int i = 0; i < lineIndexs.Length; i++)
        {
            lineRenderer.SetPosition(i, lineIndexs[i].position);
        }
    }
}

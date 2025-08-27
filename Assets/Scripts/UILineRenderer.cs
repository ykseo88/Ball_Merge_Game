using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineRenderer : MaskableGraphic
{
    public List<Vector2> Points = new();
    public float Thickness = 5f;

    private RectTransform parentRect;

    
    public void SetParent(RectTransform parent)
    {
        parentRect = parent;
        SetVerticesDirty();
    }

    public void SetPoints(IEnumerable<Vector2> newPoints)
    {
        Points.Clear();
        Points.AddRange(newPoints);
        SetVerticesDirty();
    }

    public void SetThickness(float thickness)
    {
        Thickness = thickness;
        SetVerticesDirty();
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (Points == null || Points.Count < 2)
            return;

        Matrix4x4 localMatrix = Matrix4x4.identity;

        if (parentRect != null && parentRect != rectTransform)
        {
            // parent 기준의 localPosition → 현재 rectTransform의 local 좌표계로 변환
            Vector3 parentWorldOrigin = parentRect.position;
            Quaternion parentWorldRot = parentRect.rotation;
            Vector3 parentWorldScale = parentRect.lossyScale;

            Matrix4x4 parentToWorld = Matrix4x4.TRS(parentWorldOrigin, parentWorldRot, parentWorldScale);
            Matrix4x4 worldToLocal = rectTransform.worldToLocalMatrix;

            localMatrix = worldToLocal * parentToWorld;
        }

        for (int i = 0; i < Points.Count - 1; i++)
        {
            Vector2 start = localMatrix.MultiplyPoint3x4(Points[i]);
            Vector2 end = localMatrix.MultiplyPoint3x4(Points[i + 1]);

            Vector2 dir = (end - start).normalized;
            Vector2 normal = new Vector2(-dir.y, dir.x) * (Thickness / 2f);

            Vector2 v0 = start - normal;
            Vector2 v1 = start + normal;
            Vector2 v2 = end + normal;
            Vector2 v3 = end - normal;

            int idx = vh.currentVertCount;
            vh.AddVert(v0, color, Vector2.zero);
            vh.AddVert(v1, color, Vector2.zero);
            vh.AddVert(v2, color, Vector2.zero);
            vh.AddVert(v3, color, Vector2.zero);

            vh.AddTriangle(idx + 0, idx + 1, idx + 2);
            vh.AddTriangle(idx + 2, idx + 3, idx + 0);
        }
    }

}
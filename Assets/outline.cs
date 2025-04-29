using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class outline : MonoBehaviour
{
    void Start()
    {
        GridInfo grides = transform.parent.parent.GetComponent<GridInfo>();
        LineRenderer line = GetComponent<LineRenderer>();
        CompositeCollider2D myCollider = grides.detectors.transform.GetChild(0).GetComponent<CompositeCollider2D>();
        Vector2[] points = new Vector2[myCollider.GetPathPointCount(0)];
        myCollider.GetPath(0, points);
        Vector3[] points3D = Array.ConvertAll(points, i => new Vector3(i.x, i.y, 0));
        line.positionCount = points3D.Length;
        line.SetPositions(points3D);    
    }
}

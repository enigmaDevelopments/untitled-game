using System;
using UnityEngine;

public class outline : MonoBehaviour
{
    void Start()
    {
        GameObject lineObj = transform.GetChild(0).gameObject;
        GridInfo grides = transform.parent.parent.GetComponent<GridInfo>();
         
        foreach (Transform child in grides.detectors.transform)
        {
            if (child.GetComponent<Data>().isStair)
                continue;
            CompositeCollider2D collider = child.GetComponent<CompositeCollider2D>();
            for (int i = 0; i < collider.pathCount; i++)
            {
                LineRenderer line = Instantiate(lineObj, transform).GetComponent<LineRenderer>();
                Vector2[] points = new Vector2[collider.GetPathPointCount(i)];
                collider.GetPath(i, points);
                Vector3[] points3D = Array.ConvertAll(points, i => new Vector3(i.x, i.y, 0));
                line.positionCount = points3D.Length;
                line.SetPositions(points3D);
                line.Simplify(.2f);
            }
        }
        Destroy(lineObj);
    }
}

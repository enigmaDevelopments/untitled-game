using UnityEngine;

public class TileCopier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform newLayer = Instantiate(gameObject, transform.parent).transform;
        Destroy(newLayer.GetComponent<TileCopier>());
        newLayer.name = "Collitions";
        int layer = LayerMask.NameToLayer("Default");
        newLayer.gameObject.layer = layer;
        foreach (Transform child in newLayer)
        {
            CompositeCollider2D compositeCollider = child.GetComponent<CompositeCollider2D>();
            compositeCollider.geometryType = CompositeCollider2D.GeometryType.Outlines;
            compositeCollider.isTrigger = false;
            child.gameObject.SetActive(false);
            child.gameObject.layer = layer;
            Destroy(child.GetComponent<Info>());
            Hidden hidden = child.GetComponent<Hidden>();
            if (hidden != null) 
                Destroy(hidden);
        }
    }
}

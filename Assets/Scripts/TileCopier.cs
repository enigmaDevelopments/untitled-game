using UnityEngine;
using UnityEngine.Tilemaps;

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
            compositeCollider.geometryType = 0;
            compositeCollider.isTrigger = false;
            child.GetComponent<TilemapCollider2D>().enabled = false;
            child.gameObject.layer = layer;
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxTileCreator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TilemapCollider2D>().extrusionFactor = 1;
        GameObject tiles = Instantiate(gameObject,transform);
        tiles.transform.localPosition = Vector3.zero;
        Destroy(tiles.GetComponent<BoxTileCreator>());
        Destroy(tiles.GetComponent<CompositeCollider2D>());
        Destroy(tiles.GetComponent<Rigidbody2D>());
        tiles.GetComponent<Renderer>().enabled = false;
        TilemapCollider2D collider = tiles.GetComponent<TilemapCollider2D>();
        collider.compositeOperation = Collider2D.CompositeOperation.Difference;
        collider.extrusionFactor = 0f;
    }
}

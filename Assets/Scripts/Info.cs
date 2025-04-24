using UnityEngine;

public class Info : Data
{
    public GridInfo grid;
    private void Start()
    {
        if (grid == null)
            grid = transform.parent.GetComponent<GridInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && grid.active)
        {
            collision.GetComponent<Data>().CopyTo(this);
        }
    }
}

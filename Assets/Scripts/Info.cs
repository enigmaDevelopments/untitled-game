using UnityEngine;
using System.Collections;

public class Info : Data
{
    public GridInfo grid;
    private void Start()
    {
        if (grid == null)
            grid = transform.parent.parent.GetComponent<GridInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && grid.active)
        {
            StartCoroutine(SetData(collision));
        }
    }
    private IEnumerator SetData(Collider2D player)
    {
        Debug.Log("SetData");
        yield return new WaitUntil(() => PlayerController.active);
        Debug.Log("SetData2");
        if (GetComponent<Collider2D>().IsTouching(player))
            player.GetComponent<Data>().CopyTo(this);
        yield return new WaitForEndOfFrame();
        PlayerController.active = false;
    }
}

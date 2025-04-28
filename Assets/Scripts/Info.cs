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
        yield return new WaitUntil(() => PlayerController.active);
        yield return new WaitForSecondsRealtime(.03F);
        if (GetComponent<Collider2D>().IsTouching(player) && PlayerController.active)
        {
            player.GetComponent<Data>().CopyTo(this);
            PlayerController.active = false;
        }
    }
}

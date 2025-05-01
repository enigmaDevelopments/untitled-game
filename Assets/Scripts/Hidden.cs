using UnityEngine;
using System.Collections;

public class Hidden : MonoBehaviour
{
    public Data data;
    public GridInfo grids;
    void Start()
    {
        if (grids == null)
            grids = transform.parent.parent.GetComponent<GridInfo>();
        grids.draw.SetActive(false);
        Collition(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (data.height == collision.GetComponent<Data>().height || grids.active)
            {
                Activate(collision.GetComponent<SpriteRenderer>());
                StartCoroutine(Wait(collision));
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("box"))
                foreach (Collider2D child in grids.detectors.GetComponentsInChildren<Collider2D>())
                    if (child.IsTouching(collision))
                        return;
            Collition(true);
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "main";
        }
    }
    private void Collition (bool set)
    {
        grids.draw.SetActive(!set);
        grids.active = !set;

        foreach (Collider2D child in grids.main.GetComponent<GridInfo>().draw.GetComponentsInChildren<Collider2D>())
            child.enabled = set;
    }
    private void Activate(SpriteRenderer player)
    {
        Collition(false);
        player.sortingLayerName = "main-hidden";
    }
    private IEnumerator Wait(Collider2D player)
    {
        yield return new WaitUntil(() => PlayerController.active);
        if (GetComponent<Collider2D>().IsTouching(player))
        {
            Activate(player.GetComponent<SpriteRenderer>());
        }
    }
}
using UnityEngine;
using System.Collections;

public class Hidden : MonoBehaviour
{
    public Data data;
    private GridInfo grids;
    void Start()
    {
        StartCoroutine(GetGrids());
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
            Collition(true);
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "player";
        }
    }
    private void Collition (bool set)
    {
        grids.draw.SetActive(!set);
        grids.active = !set;
        foreach (Transform child in grids.main.transform)
        {
            Collider2D[] grandchildern = child.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D grandchild in grandchildern)
                grandchild.enabled = set;
        }
    }
    private void Activate(SpriteRenderer player)
    {
        Collition(false);
        player.sortingLayerName = "player-hidden";
    }
    private IEnumerator Wait(Collider2D player)
    {
        yield return new WaitUntil(() => PlayerController.active);
        if (GetComponent<Collider2D>().IsTouching(player))
        {
            Activate(player.GetComponent<SpriteRenderer>());
        }
    }
    private IEnumerator GetGrids()
    {
        yield return new WaitForEndOfFrame();
        grids = transform.parent.parent.GetComponent<GridInfo>();
        grids.draw.SetActive(false);
    }
}
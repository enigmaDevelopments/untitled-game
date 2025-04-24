using UnityEngine;
using System.Collections;

public class Hidden : MonoBehaviour
{
    public Data data;
    private GridInfo grids;
    void Start()
    {
        grids = transform.parent.GetComponent<GridInfo>();
        grids.draw.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (data.height == collision.GetComponent<Data>().height)
            {
                grids.draw.SetActive(true);
                Collition(false);
                collision.GetComponent<SpriteRenderer>().sortingLayerName = "player-hidden";
                grids.active = true;
                StartCoroutine(ResetData(collision.GetComponent<Data>()));
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            grids.draw.SetActive(false);
            Collition(true);
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "player";
            grids.active = false;
        }
    }
    private void Collition (bool set)
    {
        foreach (Transform child in grids.main.transform)
        {
            Collider2D[] grandchildern = child.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D grandchild in grandchildern)
                grandchild.enabled = set;
        }
    }
    private IEnumerator ResetData(Data player)
    {
        yield return new WaitForEndOfFrame();
        player.CopyTo(data);
    }
}
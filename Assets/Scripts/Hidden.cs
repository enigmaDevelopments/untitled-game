using UnityEditor.PackageManager;
using UnityEngine;

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
                grids.draw.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            grids.draw.SetActive(false);
    }
}

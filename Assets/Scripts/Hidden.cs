using UnityEngine;

public class Hidden : MonoBehaviour
{
    public GameObject draw;
    public Data data;
    void Start()
    {
        draw.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (data.height == collision.GetComponent<Data>().height)
                draw.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            draw.SetActive(false);
    }
}

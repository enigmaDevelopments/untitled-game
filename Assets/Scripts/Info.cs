using UnityEngine;

public class Info : Data
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Data>().CopyTo(this);
        }
    }
}

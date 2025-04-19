using UnityEngine;

public class Info : Data
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Data player = collision.GetComponent<Data>();
            player.isStair = isStair;
            player.height = height;
        }
    }
}

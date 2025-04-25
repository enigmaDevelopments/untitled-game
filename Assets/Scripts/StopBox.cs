using UnityEngine;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().pushingBox = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().pushingBox = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

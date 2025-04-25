using UnityEngine;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

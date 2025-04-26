using UnityEngine;

public class StopBox : MonoBehaviour
{
    public bool hidden = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private PlayerController playerController;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        spriteRenderer.sortingLayerName = (player.position.y < transform.position.y ? "collition" : "walk behind") + (hidden ? "-hidden" : "");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.pushingBox = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.pushingBox = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

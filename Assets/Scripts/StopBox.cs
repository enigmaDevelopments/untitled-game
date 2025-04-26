using UnityEngine;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Data data;
    public bool hidden = false;
    private Transform player;
    private PlayerController playerController;
    private Data playerData;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        playerData = player.GetComponent<Data>();
    }
    private void Update()
    {
        spriteRenderer.sortingLayerName = (player.position.y < transform.position.y-.05f || data.height == playerData.height? "collition" : "walk behind") + (hidden ? "-hidden" : "");
    }
    private void FixedUpdate()
    {
        transform.parent.position = transform.position;
        transform.localPosition = Vector2.zero;
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

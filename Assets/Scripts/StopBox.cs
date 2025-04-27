using UnityEngine;
using System.Collections;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D baseCollider;
    public Collider2D differenceCollider;
    public SpriteRenderer spriteRenderer;
    public Data data;
    public GridInfo grids;
    public bool hidden = false;
    private CompositeCollider2D compositeCollider;
    private Transform topCollition;
    private Transform player;
    private PlayerController playerController;
    private Data playerData;
    private void Start()
    {
        if (grids == null)
            grids = transform.parent.parent.parent.GetComponent<GridInfo>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        playerData = player.GetComponent<Data>();
        StartCoroutine(MakeTopCollition());
    }
    private void Update()
    {
        spriteRenderer.sortingLayerName = (player.position.y < transform.position.y-.05f || data.height == playerData.height? "collition" : "walk behind") + (hidden ? "-hidden" : "");
    }
    private void FixedUpdate()
    {
        if (compositeCollider == null)
            return;
        transform.parent.position = transform.position;
        transform.localPosition = Vector2.zero;
        topCollition.position = transform.position;
        differenceCollider.enabled = playerData.height == data.height;
        compositeCollider.GenerateGeometry();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && data.height == playerData.height)
        {
            topCollition.parent.gameObject.SetActive(true);
            baseCollider.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            topCollition.parent.gameObject.SetActive(false);
            baseCollider.enabled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController.active = false;
    }
    private IEnumerator MakeTopCollition()
    {
        yield return new WaitForEndOfFrame();
        topCollition = Instantiate(transform.parent.gameObject, grids.collition.transform.GetChild(data.height)).transform;
        Destroy(topCollition.GetComponent<SpriteRenderer>());
        foreach (Transform child in topCollition.transform)
            Destroy(child.gameObject);
        Collider2D topCollider = topCollition.GetComponent<Collider2D>();
        topCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
        topCollider.enabled = true;
        compositeCollider = grids.draw.GetComponent<CompositeCollider2D>();
    }
}

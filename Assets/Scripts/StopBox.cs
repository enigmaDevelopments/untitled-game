using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D baseCollider;
    public BoxCollider2D baseCollider2;
    public Collider2D differenceCollider;
    public Transform base2;
    public SpriteRenderer spriteRenderer;
    public Data data;
    public GridInfo grids;
    public bool hidden = false;
    private CompositeCollider2D compositeCollider;
    private Transform topCollition;
    private Transform player;
    private PlayerController playerController;
    private Rigidbody2D playerRb;
    private Data playerData;
    private bool onBox = false;
    private bool isVertical;
    private int timer = 0;
    private void Start()
    {
        if (grids == null)
            grids = transform.parent.parent.parent.GetComponent<GridInfo>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        playerData = player.GetComponent<Data>();
        playerRb = player.GetComponent<Rigidbody2D>();
        StartCoroutine(MakeTopCollition());
    }
    private void Update()
    {
        spriteRenderer.sortingLayerName = (player.position.y < transform.position.y || data.height == playerData.height? "collition" : "walk behind") + (hidden ? "-hidden" : "");
    }
    private void FixedUpdate()
    {
        if (compositeCollider == null)
            return;
        transform.parent.position = transform.position;
        transform.localPosition = Vector2.zero;
        topCollition.position = transform.position;
        bool playerAbove = playerData.height == data.height;
        differenceCollider.enabled = playerAbove;
        baseCollider.enabled = !playerAbove;
        compositeCollider.GenerateGeometry();
        if (onBox)
        {
            PlayerController.active = false;
            topCollition.parent.gameObject.SetActive(true);
            playerData.height = data.height;
        }
        isVertical = Mathf.Abs(transform.position.x - player.position.x) < Mathf.Abs(transform.position.y - player.position.y - .5f);

        if (0 < timer)
        {
            baseCollider2.size = new Vector2(isVertical ? .6f : .975f, isVertical ? .975f : .6f);
            if (1000 < timer)
                timer = -100;

        }
        else
        {
            baseCollider2.size = new Vector2(.975f, .975f);
        }

        timer++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("box"))
        {
            if (collision.gameObject.CompareTag("Player"))
                playerController.pushingBox = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("box"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation| (isVertical ? RigidbodyConstraints2D.FreezePositionX: RigidbodyConstraints2D.FreezePositionY);
            if (collision.gameObject.CompareTag("Player"))
            {
                playerController.pushingBox = true;
                playerController.boxHorzontal = !isVertical;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && data.height == playerData.height)
        {
            onBox = true;
            topCollition.parent.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onBox = false;
            topCollition.parent.gameObject.SetActive(false);
        }
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

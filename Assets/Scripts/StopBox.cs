using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class StopBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D baseCollider;
    public BoxCollider2D baseCollider2;
    public Collider2D differenceCollider;
    public Transform top;
    public Transform base2;
    public SpriteRenderer spriteRenderer;
    public Data data;
    public GridInfo grids;
    public bool hidden = false;
    private CompositeCollider2D compositeCollider;
    private Transform topCollition;
    private Transform player;
    private PlayerController playerController;
    private Data playerData;
    private bool onBox = false;
    private bool isVertical;
    private void Start()
    {
        if (grids == null)
            grids = transform.parent.parent.parent.GetComponent<GridInfo>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        playerData = player.GetComponent<Data>();
        topCollition = Instantiate(transform.parent.gameObject, grids.collition.transform.GetChild(data.height)).transform;
        Destroy(topCollition.GetComponent<SpriteRenderer>());
        foreach (Transform child in topCollition.transform)
            Destroy(child.gameObject);
        Collider2D topCollider = topCollition.GetComponent<Collider2D>();
        topCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
        topCollider.enabled = true;
        compositeCollider = grids.draw.GetComponent<CompositeCollider2D>();
        top.parent = grids.detectors.transform;
        if  (hidden)
        {
            Hidden hidden = top.AddComponent<Hidden>();
            hidden.data = data;
            hidden.grids = grids;
        }
    }
    private void Update()
    {
        spriteRenderer.sortingLayerName = "walk " + (player.position.y < transform.position.y || data.height == playerData.height ? "in front" : "behind") + (hidden ? "-hidden" : "");
    }
    private void FixedUpdate()
    {
        if (compositeCollider == null)
            return;
        transform.parent.position = transform.position;
        transform.localPosition = Vector2.zero;
        topCollition.position = transform.position;
        top.position = transform.position;
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
        baseCollider2.size = new Vector2(isVertical ? .9f : .975f, isVertical ? .975f : .9f);
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
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | (isVertical ? RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY);
            if (collision.gameObject.CompareTag("Player"))
            {
                playerController.pushingBox = true;
                playerController.boxHorzontal = !isVertical;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger(collision, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Trigger(collision, false);
    }
    public void Trigger(Collider2D collision,bool enter)
    {
        if (collision.gameObject.CompareTag("Player") && data.height == playerData.height)
        {
            onBox = enter;
            topCollition.parent.gameObject.SetActive(enter);
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Data data;
    public LayerMask dataLayer;
    public float speed = 5f;
    public Vector2 lastMovmenmt = Vector2.zero;

    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(Horizontal, Vertical);
        movement = Vector2.ClampMagnitude(movement, 1);
        rb.linearVelocity = movement * speed;
        
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
        if (movement != Vector2.zero)
        {
            lastMovmenmt = movement;
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
        animator.SetFloat("lastX", lastMovmenmt.x);
        animator.SetFloat("lastY", lastMovmenmt.y);
        animator.SetFloat("speed", movement.magnitude * 15f);

        if (Input.GetButtonDown("Jump")) {
            if (data.isStair)
                return;
            Vector2 pos = transform.position + (Vector3)(lastMovmenmt.normalized * 3);
            pos = new Vector2(Mathf.Floor(pos.x) + .5f, Mathf.Floor(pos.y) +.5f);
            Collider2D[] nodes = Physics2D.OverlapPointAll(pos, dataLayer);
            if (nodes == null)
                return;
            foreach (Collider2D node in nodes)
            {
                Data info = node.GetComponent<Info>();
                if (info.isStair || data.height != info.height)
                    continue;
                transform.position = pos;
                break;
            }
        }
    }
}


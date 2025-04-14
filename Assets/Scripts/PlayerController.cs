using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Data data;
    public LayerMask dataLayer;
    public float speed = 5f;


    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(Horizontal, Vertical);
        movement = Vector2.ClampMagnitude(movement, 1);
        rigidbody.linearVelocity = movement * speed;
        if (Input.GetButtonDown("Jump")) {
            Vector2 pos = transform.position + (Vector3)(movement.normalized * 3);
            pos.y -= .5f;
            pos = new Vector2(Mathf.Floor(pos.x) + .5f, Mathf.Floor(pos.y) + .5f);
            Collider2D node = Physics2D.OverlapPoint(pos, dataLayer);
            if (node == null)
                return;
            Data info = node.GetComponent<Info>();
            if (data.height != info.height||data.isStair||info.isStair)
                return;
            pos.y += .5f;
            transform.position = pos;
        }
    }
}

using UnityEditor;
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
        if (Input.GetKeyDown(KeyCode.Space)){
            Vector3 Dir = movement.normalized * 3;
            Data node = Physics2D.OverlapPoint(transform.position + Dir, dataLayer).GetComponent<Info>();
            if (node == null)
                return;
            if (data.height != node.height)
                return;
            transform.position += Dir;
        }
    }
}

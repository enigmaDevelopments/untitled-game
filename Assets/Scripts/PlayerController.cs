using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed = 5f;
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(Horizontal, Vertical);
        movement = Vector2.ClampMagnitude(movement, 1);
        rigidbody.linearVelocity = movement * Time.deltaTime * speed;
    }
}

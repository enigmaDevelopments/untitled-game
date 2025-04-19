using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    public float speed = 5f;
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(Horizontal, Vertical);
        movement = Vector2.ClampMagnitude(movement, 1);
        rigidbody.linearVelocity = movement * speed * Time.deltaTime;
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
        if (movement != Vector2.zero)
        {
            animator.SetFloat("lastX", movement.x);
            animator.SetFloat("lastY", movement.y);
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
    }
}


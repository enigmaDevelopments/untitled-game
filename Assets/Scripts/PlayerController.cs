using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Android;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Data data;
    public LayerMask dataLayer;
    public float speed = 5f;
    public float boxSpeed = 1f;
    public float stairSpeedMultiplier = .5f;
    public bool pushingBox = false;
    public bool boxHorzontal = false;
    public static bool active = false;
    private Vector2 lastMovmenmt = Vector2.zero;

    void Update()
    {

        Vector2 currentSpeed = new Vector2(pushingBox && boxHorzontal ? boxSpeed : speed, pushingBox && !boxHorzontal ? boxSpeed : speed);
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(Horizontal, Vertical);
        movement = Vector2.ClampMagnitude(movement, 1);
        movement *= currentSpeed;
        if (data.isStair)
        {
            if (data.stairAngle == 0)
                movement.y *= stairSpeedMultiplier;
            else
                movement.x *= stairSpeedMultiplier;
            movement = Quaternion.Euler(0f, 0f, data.stairAngle) * movement;
        }

        rb.linearVelocity = movement;

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
        animator.SetFloat("speed", movement.magnitude * 6f);

        if (Input.GetButtonDown("Jump"))
        {
            if (data.isStair)
                return;
            Vector2 pos = new Vector2(transform.position.x + MathF.Sign(lastMovmenmt.x) * 3, transform.position.y + MathF.Sign(lastMovmenmt.y) * 3);
            pos = new Vector2(Mathf.Floor(pos.x) + .5f, Mathf.Floor(pos.y) + .5f);
            Collider2D[] nodes = Physics2D.OverlapPointAll(pos, dataLayer);
            foreach (Collider2D node in nodes)
            {
                Data info = node.GetComponent<Data>();
                if (info.isStair || data.height != info.height)
                    continue;
                transform.position = pos;
                break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Data info = collision.GetComponent<Data>();
        if (info == null)
            return;
        if (info.height == data.height && info.isStair == data.isStair && info.stairAngle == data.stairAngle)
            active = true;
    }
}


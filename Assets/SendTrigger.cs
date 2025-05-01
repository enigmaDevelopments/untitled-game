using UnityEngine;

public class SendTrigger : MonoBehaviour
{
    private StopBox parent;
    private void Awake()
    {
        parent = GetComponentInParent<StopBox>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.Trigger(collision, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.Trigger(collision, false);
    }
}

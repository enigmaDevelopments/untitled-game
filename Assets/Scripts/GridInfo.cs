using UnityEngine;

public class GridInfo : MonoBehaviour
{
    public GameObject main;
    public GameObject draw;
    public GameObject collition;
    public bool active = true;
    private void Start()
    {
        if (draw == null)
            draw = transform.GetChild(0).gameObject;
        if (collition == null)
            collition = transform.GetChild(2).gameObject;
    }
}

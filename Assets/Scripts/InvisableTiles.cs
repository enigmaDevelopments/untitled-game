using UnityEngine;

public class InvisableTiles : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}

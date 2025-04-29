using UnityEngine;

public class Invisable : MonoBehaviour
{
    void Start()
    {
        Destroy(GetComponent<Renderer>());
    }
}

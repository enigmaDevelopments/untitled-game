using UnityEngine;

public class dummy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDisable()
    {
        Debug.Log("GameObject is disabled");
    }
}

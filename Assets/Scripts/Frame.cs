using UnityEngine;
using UnityEngine.Rendering;

public class Frame : MonoBehaviour
{
    public float top;
    public float bottom;
    public float left;
    public float right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Assert(left < right && bottom < top, "Invalid frame size");
        float x = (left + right)/2;
        float y = (bottom + top)/2;
        float height = top - bottom;
        float width = right - left;
        float aspectRatio = Screen.width / ((float)Screen.height);
        float widthRatio = width/aspectRatio;

        transform.position = new Vector3(x, y, -10);

        if (height < widthRatio)

            gameObject.GetComponent<Camera>().orthographicSize = widthRatio / 2;
        else
            gameObject.GetComponent<Camera>().orthographicSize = height / 2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Data : MonoBehaviour
{
    public int height = 0;
    public float stairAngle = 0f;
    public bool isStair = false;

    public void CopyTo(Data data)
    {
        height = data.height;
        stairAngle = data.stairAngle;
        isStair = data.isStair;
    }
}
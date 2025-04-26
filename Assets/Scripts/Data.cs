using UnityEngine;

public class Data : MonoBehaviour
{
    public float height;
    public float stairAngle;
    public bool isStair;

    public void CopyTo(Data data)
    {
        height = data.height;
        stairAngle = data.stairAngle;
        isStair = data.isStair;
    }
}
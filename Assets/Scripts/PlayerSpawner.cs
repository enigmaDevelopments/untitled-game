using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerAsset;
    public static Vector3 playerPos;
    public static int height;
    public static float stairAngle;
    public static bool isStair;
    void Awake()
    {
        GameObject player = Instantiate(playerAsset, playerPos, Quaternion.identity);
        Data playerData = player.GetComponent<Data>();
        playerData.height = height;
        playerData.stairAngle = stairAngle;
        playerData.isStair = isStair;
    }
}

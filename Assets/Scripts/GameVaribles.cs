using UnityEngine;

public class GameVaribles : MonoBehaviour
{
    public GameObject playerAsset;
    public static Vector3 playerPos;
    public static Data playerData;
    void Awake()
    {
        GameObject player = Instantiate(playerAsset, playerPos, Quaternion.identity);
        if (playerData != null)
            player.GetComponent<Data>().CopyTo(playerData);
    }
}

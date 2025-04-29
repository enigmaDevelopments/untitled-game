using UnityEngine;

public class GameVaribles : MonoBehaviour
{
    public GameObject playerAsset;
    public static Vector3 playePos;
    public static Data playerData;
    void Awake()
    {
        GameObject player = Instantiate(playerAsset, playePos, Quaternion.identity);
        if (playerData != null)
            player.GetComponent<Data>().CopyTo(playerData);
    }
}

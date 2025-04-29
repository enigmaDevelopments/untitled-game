using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public Data startingData;
    public string sceneName;
    public Vector2 startingPos;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameVaribles.playerData = startingData;
            GameVaribles.playerPos = startingPos;
            SceneManager.LoadScene(sceneName);
        }
    }
}

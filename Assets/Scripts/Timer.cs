using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float time = 120f;
    public float timer = 120f;
    private static bool isTimerActive = false;
    public static bool kill = false;
    void Start()
    {
        if (isTimerActive)
            Destroy(gameObject);
        else
        {
            isTimerActive = true;
            timer = time;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (time <= 60)
            timerText.text = $"{(int)timer}";
        else
            timerText.text = string.Format("{0:0}:{1:00}", (int)(timer / 60), (int)(timer % 60));
        if (timer <= 0)
        {
            timerText.text = "0:00";
            SceneManager.LoadScene(0);
            timer = time;
            PlayerSpawner.playerPos = Vector2.zero;
            PlayerSpawner.height = 0;
            PlayerSpawner.stairAngle = 0;
            PlayerSpawner.isStair = false;
        }
        if (kill)
            Destroy(gameObject);
    }
}

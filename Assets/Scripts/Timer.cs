using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public static float timer = 120f;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        timerText.text = $"{minutes}:{seconds}";

    }
}

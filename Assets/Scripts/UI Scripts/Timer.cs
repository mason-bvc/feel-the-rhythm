using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timerText;
    void Update()
    {
        timer += Time.deltaTime;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI TimerText;

    public float ElapsedTime;

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(ElapsedTime / 60);
        int seconds = Mathf.FloorToInt(ElapsedTime % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetElapsedTime()
    {
        return ElapsedTime;
    }
}

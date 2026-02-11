using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private static float bestTime = float.MaxValue;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);  
    }

    public static void UpdateBestTime(float time)
    {
        if (time < bestTime)
        {
            bestTime = time;
        }
    }

    public void ShowBestTimeText(TextMeshProUGUI bestTimeText)
    {
        if (bestTime == float.MaxValue)
        {
            bestTimeText.text = "Best time is: --:--";
            return;
        }

        int minutes = Mathf.FloorToInt(bestTime / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);

        bestTimeText.text = "Best time is: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

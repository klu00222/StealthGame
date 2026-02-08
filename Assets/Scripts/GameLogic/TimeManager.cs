using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public static float BestTime = float.MaxValue;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);  
    }

    public static void UpdateBestTime(float Time)
    {
        if (Time < BestTime)
        {
            BestTime = Time;
        }
    }

    public void ShowBestTimeText(TextMeshProUGUI BestTimeText)
    {

        if (BestTime == float.MaxValue)
        {
            BestTimeText.text = "Best time is: --:--";
            return;
        }

        int minutes = Mathf.FloorToInt(BestTime / 60);
        int seconds = Mathf.FloorToInt(BestTime % 60);

        BestTimeText.text = "Best time is: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


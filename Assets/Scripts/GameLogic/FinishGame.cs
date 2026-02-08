using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Timer timer = Object.FindFirstObjectByType<Timer>();
            TimeManager.UpdateBestTime(timer.GetElapsedTime());
        }

        SceneManager.LoadScene("Ending");
    }
}

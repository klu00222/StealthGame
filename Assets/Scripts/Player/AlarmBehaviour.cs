using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject Alarm;

    private void OnEnable()
    {
        EnemyData.OnDetectionChanged += ToggleIcon;
    }

    private void OnDisable()
    {
        EnemyData.OnDetectionChanged -= ToggleIcon;
    }

    private void ToggleIcon(bool seen)
    {
        if (Alarm != null)
        {
            Alarm.SetActive(seen);
        }
    }
}

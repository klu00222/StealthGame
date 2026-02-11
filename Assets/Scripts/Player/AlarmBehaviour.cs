using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    public GameObject alarm;
    private SpriteRenderer alarmSpriteRenderer;

    public void Awake()
    {
        alarmSpriteRenderer = alarm.GetComponent<SpriteRenderer>();
    }

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
        if (alarm != null)
        {
            alarmSpriteRenderer.enabled = seen;
        }
    }
}

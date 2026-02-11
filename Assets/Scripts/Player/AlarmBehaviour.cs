using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    public GameObject Alarm;
    private SpriteRenderer alarmSpriteRenderer;

    public void Awake()
    {
        alarmSpriteRenderer = Alarm.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ChaseBehaviour.OnChasingChange += ToggleIcon;
    }

    private void OnDisable()
    {
        ChaseBehaviour.OnChasingChange -= ToggleIcon;
    }

    private void ToggleIcon(bool seen)
    {
        if (Alarm != null)
        {
            alarmSpriteRenderer.enabled = seen;
        }
    }
}

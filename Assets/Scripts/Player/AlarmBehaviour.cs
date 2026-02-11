using UnityEngine;
using System;

public class AlarmBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject Alarm;
    public SpriteRenderer alarmSpriteRenderer;

    public void Awake()
    {
        Alarm = gameObject.transform.Find("Alarm").gameObject;
        alarmSpriteRenderer = Alarm.GetComponent<SpriteRenderer>();
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
        if (Alarm != null)
        {
            alarmSpriteRenderer.enabled = (seen);
        }
    }
}

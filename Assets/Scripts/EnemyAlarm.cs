using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour
{
    SpriteRenderer alarmRenderer;

    public void PlayerDetected()
    {
        ChangeColor(Color.red);
    }

    public void PlayerLeft()
    {
        ChangeColor(new Color(0,0,0,0));
    }

    private void ChangeColor(Color color)
    {
        if (alarmRenderer == null) alarmRenderer = GetComponent<SpriteRenderer>();

        alarmRenderer.color = color;
    }
}

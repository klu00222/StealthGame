using System;
using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2.0f;
    public float time = 2.0f;

    public Transform WaypointsParent;
    private Transform[] waypoints;
    public bool loopWaypoints = true;

    private int currentIndex;
    private bool waiting;

    private void Start()
    {
        waypoints = new Transform[WaypointsParent.childCount];

        for (int i = 0; i < WaypointsParent.childCount; i++)
        {
            waypoints[i] = WaypointsParent.GetChild(i);
        }
    }

    private void Update()
    {
        if (waiting) return;
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWayPoint());
        }
    }

    IEnumerator WaitAtWayPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(time);

        if (loopWaypoints)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        } else
        {
            Math.Min(currentIndex + 1, waypoints.Length - 1);
        }

        waiting = false;
    }
}

using System;
using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float time = 2.0f;

    [SerializeField]
    private bool loopWaypoints = true;
    public Transform WaypointsParent;
    private Transform[] waypoints;

    private int currentIndex;
    private bool waiting;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        waypoints = new Transform[WaypointsParent.childCount];

        for (int i = 0; i < WaypointsParent.childCount; i++)
        {
            waypoints[i] = WaypointsParent.GetChild(i);
        }
    }

    private void FixedUpdate()
    {
        if (waiting)
        {
            return;
        }

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = waypoints[currentIndex];
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target.position) < 0.1f)
        {
            _ = StartCoroutine(WaitAtWayPoint());
        }
    }

    private IEnumerator WaitAtWayPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(time);

        if (loopWaypoints)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
        else
        {
            _ = Math.Min(currentIndex + 1, waypoints.Length - 1);
        }

        waiting = false;
    }
}

using System;
using UnityEngine;


public class EnemyData : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float Speed = 2.0f;
    public float RotationSpeed = 10f;
    public float WaitTime = 2.0f;
    public Transform[] Waypoints;
    [SerializeField] private Transform waypointsParent;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 2.2f;
    [SerializeField] private float visionAngle = 45f;
    [SerializeField] private LayerMask obstacleMask;

    public bool IsWaiting = false;
    public float Timer;
    public int CurrentIndex;

    private Rigidbody2D rb;
    private Transform player;

    public static event Action<bool> OnDetectionChanged;
    private bool wasPlayerVisible;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        if (waypointsParent != null)
        {
            Waypoints = new Transform[waypointsParent.childCount];
            for (int i = 0; i < waypointsParent.childCount; i++)
            {
                Waypoints[i] = waypointsParent.GetChild(i);
            }
        }
    }

    public bool CanSeePlayer()
    {
        if (player == null)
        {
            return false;
        }

        //Distance from player calculation
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            UpdateDetectionState(false);
            return false;
        }

        Vector2 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        if (angleToPlayer > visionAngle / 2f)
        {
            UpdateDetectionState(false);
            return false;
        }

        //Check if an object is in the way (can't see player)
        Vector2 rayStart = (Vector2)transform.position + (directionToPlayer.normalized * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, directionToPlayer, distance, obstacleMask);

        if (hit.collider != null)
        {
            UpdateDetectionState(false);
            return false; //hit obstacle
        }

        UpdateDetectionState(true);
        return true;
    }

    private void UpdateDetectionState(bool isCurrentlyVisible)
    {
        if (isCurrentlyVisible != wasPlayerVisible)
        {
            wasPlayerVisible = isCurrentlyVisible;
            OnDetectionChanged?.Invoke(isCurrentlyVisible);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.AngleAxis(visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * detectionRange);

        Vector3 direction2 = Quaternion.AngleAxis(-visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * detectionRange);

        Gizmos.color = Color.white;
    }
}

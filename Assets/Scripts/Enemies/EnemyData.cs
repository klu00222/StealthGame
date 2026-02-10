using System;
using UnityEngine;


public class EnemyData : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed = 2.0f;
    public float rotationSpeed = 10f;
    [SerializeField] private Transform waypointsParent;
    public float waitTime = 2.0f;
    [SerializeField] private bool loopWaypoints = true;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 2.2f;
    [SerializeField] private float visionAngle = 45f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform hitDetector;

    public Transform[] waypoints;
    public bool isWaiting = false;
    public float timer;
    public int currentIndex;
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
            waypoints = new Transform[waypointsParent.childCount];
            for (int i = 0; i < waypointsParent.childCount; i++)
            {
                waypoints[i] = waypointsParent.GetChild(i);
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
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float Speed = 2.0f;
    public float RotationSpeed = 10f;

    public Transform[] Waypoints;
    public Transform WaypointsParent;
    public int CurrentIndex;

    [Header("Detection")]
    public float DetectionRange = 3.5f;
    public float VisionAngle = 60.0f;
    [SerializeField]
    private LayerMask obstacleMask;

    [Header("Wait Settings")]
    public bool PatrolIsWaiting = false;
    public float PatrolWaitTime = 2.0f;
    public float PatrolTimer;

    public float ExposureWaitTime = 1.0f;
    public float ExposureTimer;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (WaypointsParent != null)
        {
            Waypoints = new Transform[WaypointsParent.childCount];
            for (int i = 0; i < WaypointsParent.childCount; i++)
            {
                Waypoints[i] = WaypointsParent.GetChild(i);
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

        if (distance > DetectionRange)
        {
            return false;
        }

        Vector2 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        if (angleToPlayer > VisionAngle / 2f)
        {
            return false;
        }

        //Check if an object is in the way (can't see player)
        Vector2 rayStart = (Vector2)transform.position + (directionToPlayer.normalized * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, directionToPlayer, DetectionRange, obstacleMask);

        if (hit.collider != null)
        {
            return false; //Hit obstacle
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.AngleAxis(VisionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);

        Vector3 direction2 = Quaternion.AngleAxis(-VisionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }
}

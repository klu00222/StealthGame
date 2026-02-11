using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private EnemyData data;
    private Rigidbody2D rb;

    private static readonly int isPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int isChasing = Animator.StringToHash("isChasing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data = animator.GetComponent<EnemyData>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (data == null || data.Waypoints.Length == 0)
        {
            return;
        }

        if (data.CanSeePlayer())
        {
            animator.SetBool(isChasing, true);
            animator.SetBool(isPatrolling, false);
            return;
        }

        if (data.IsWaiting)
        {
            data.Timer += Time.deltaTime;

            if (data.Timer >= data.WaitTime)
            {
                data.Timer = 0;
                data.IsWaiting = false;

                //Modulo wrapping (cycle through waypoints)
                data.CurrentIndex = (data.CurrentIndex + 1) % data.Waypoints.Length;
            }

            return;
        }

        //Movement
        Transform target = data.Waypoints[data.CurrentIndex];
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, data.Speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        //Rotation
        Vector2 moveDirection = ((Vector2)target.position - rb.position).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, data.RotationSpeed * Time.deltaTime));
            //Quaternion rotation = Quaternion.Euler(0, 0, angle);

            //animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, rotation, data.RotationSpeed * Time.deltaTime);
        }

        //Waypoint reached check
        if (Vector2.Distance(rb.position, target.position) < 0.1f)
        {
            data.IsWaiting = true;
        }
    }
}

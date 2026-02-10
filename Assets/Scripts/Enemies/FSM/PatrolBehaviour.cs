using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private EnemyData data;

    private static readonly int isPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int isChasing = Animator.StringToHash("isChasing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data = animator.GetComponent<EnemyData>();
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
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.position, data.Speed * Time.deltaTime);

        //Rotation
        Vector2 moveDirection = (target.position - animator.transform.position).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, rotation, data.RotationSpeed * Time.deltaTime);
        }

        //Waypoint reached check
        if (Vector2.Distance(animator.transform.position, target.position) < 0.1f)
        {
            data.IsWaiting = true;
        }
    }
}

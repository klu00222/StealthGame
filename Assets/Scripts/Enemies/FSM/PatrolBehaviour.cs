using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{

    private EnemyData data;
    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int IsChasing = Animator.StringToHash("isChasing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data = animator.GetComponent<EnemyData>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (data == null || data.waypoints.Length == 0)
        {
            return;
        }

        if (data.CanSeePlayer())
        {
            animator.SetBool(IsChasing, true);
            animator.SetBool(IsPatrolling, false);
            return;
        }

        if (data.isWaiting)
        {
            data.timer += Time.deltaTime;
            if (data.timer >= data.waitTime)
            {
                data.timer = 0;
                data.isWaiting = false;

                //Modulo wrapping (cycle through waypoints)
                data.currentIndex = (data.currentIndex + 1) % data.waypoints.Length;
            }
            return;
        }

        //Movement
        Transform target = data.waypoints[data.currentIndex];
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.position, data.speed * Time.deltaTime);

        //Rotation
        Vector2 moveDirection = (target.position - animator.transform.position).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, rotation, data.rotationSpeed * Time.deltaTime);
        }

        //Waypoint reached check
        if (Vector2.Distance(animator.transform.position, target.position) < 0.1f)
        {
            data.isWaiting = true;
        }

    }


}

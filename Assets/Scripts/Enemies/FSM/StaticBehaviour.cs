using UnityEngine;

public class StaticBehaviour : StateMachineBehaviour
{
    private EnemyData data;

    // Static is the same as patrolling in this behaviour only it is in one place
    private static readonly int isPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int isChasing = Animator.StringToHash("isChasing");

    private Vector3 firstPos = Vector3.zero;
    private bool moveToFirstPos = false;

    private readonly float[] angles = { 45.0f, 135.0f, 225.0f, 315.0f };

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data = animator.GetComponentInParent<EnemyData>();
        data.IsWaiting = false;

        if (firstPos == Vector3.zero)
        {
            firstPos = animator.transform.position;
        }
        else if (Vector3.Distance(firstPos, animator.transform.position) > 0.1f)
        {
            moveToFirstPos = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (data == null)
        {
            return;
        }

        if (moveToFirstPos)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, firstPos, data.Speed * Time.deltaTime);

            if (Vector3.Distance(firstPos, animator.transform.position) < 0.1f)
            {
                moveToFirstPos = false;
            }
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

                //Modulo wrapping (cycle through angles)
                data.CurrentIndex = (data.CurrentIndex + 1) % angles.Length;
            }

            return;
        }
        else
        {
            float targetAngle = angles[data.CurrentIndex];
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, targetRotation, data.RotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(animator.transform.rotation, targetRotation) < 1.0f)
            {
                animator.transform.rotation = targetRotation;
                data.IsWaiting = true;
            }
        }
    }
}

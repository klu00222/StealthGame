using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float stayTime = 3.0f;

    private float timer;
    private EnemyData data;

    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int IsChasing = Animator.StringToHash("isChasing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        timer = 0.0f;
        data = animator.GetComponent<EnemyData>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo state, int layer)
    {
        if (data == null)
        {
            return;
        }

        if (data.CanSeePlayer())
        {
            animator.SetBool(IsChasing, true);
            return;
        }

        timer += Time.deltaTime;
        if (timer >= stayTime)
        {
            animator.SetBool(IsPatrolling, true);
        }

    }
}

using UnityEngine;

public class StaticBehaviour : StateMachineBehaviour
{
    EnemyData data;

    private readonly int isChasingHash = Animator.StringToHash("isChasing");
    private readonly int isStaticHash = Animator.StringToHash("isStatic");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data = animator.GetComponent<EnemyData>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (data == null) return;

        if (data.CanSeePlayer())
        {
            animator.SetBool(isChasingHash, true);
            animator.SetBool(isStaticHash, false);

            return;
        }
    }
}

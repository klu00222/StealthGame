using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;

    private Transform player;
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo state, int layer)
    {
        bool playerClose = IsPlayerClose(animator.transform);
        bool timeUp = IsTimeUp();

        animator.SetBool("Chase", playerClose);
        animator.SetBool("Patrol", timeUp);
    }

    private bool IsPlayerClose(Transform transform)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return (distance < VisionRange);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return (timer > StayTime);
    }
}
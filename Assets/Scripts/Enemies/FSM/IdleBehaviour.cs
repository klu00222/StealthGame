using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float stayTime = 3.0f;

    private Transform player;
    private float timer;
    private bool playerClose;

    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int IsChasing = Animator.StringToHash("isChasing");

    private void OnEnable()
    {
        VisionDetector.OnPlayerDetected += IsPlayerClose;
    }

    private void OnDisable()
    {
        VisionDetector.OnPlayerDetected -= IsPlayerClose;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerClose = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo state, int layer)
    {
        bool timeUp = IsTimeUp();

        animator.SetBool(IsPatrolling, timeUp);
        animator.SetBool(IsChasing, playerClose);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return timer > stayTime;
    }

    private void IsPlayerClose()
    {
        playerClose = true;
    }
}

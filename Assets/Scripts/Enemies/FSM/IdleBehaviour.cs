using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public float StayTime = 3.0f;

    private Transform player;
    private float timer;
    
    bool playerClose;

    private void OnEnable()
    {
        VisionDetector.OnPlayerDetected += IsPlayerClose;
    }

    private void OnDisable()
    {
        VisionDetector.OnPlayerDetected -= IsPlayerClose;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerClose = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo state, int layer)
    {
        bool timeUp = IsTimeUp();

        animator.SetBool("isPatrolling", timeUp);
        animator.SetBool("isChasing", playerClose);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return (timer > StayTime);
    }

    private void IsPlayerClose()
    {
        playerClose = true;
    }
}

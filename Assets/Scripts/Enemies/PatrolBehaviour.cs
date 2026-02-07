using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;

    private float timer;
    private Transform player;

    private Vector2 targetPosition;
    private Vector2 startPosition;

    private float direction = 1.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPosition = new Vector2(animator.transform.position.x, animator.transform.position.y);
        targetPosition = GetTargetPosition();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool playerClose = IsPlayerClose(animator.transform);
        bool timeUp = IsTimeUp();

        animator.SetBool("Chase", playerClose);
        animator.SetBool("Patrol", !timeUp);

        animator.transform.position = Vector2.Lerp(startPosition, targetPosition, timer / StayTime);
    }

    private Vector2 GetTargetPosition()
    {
        targetPosition = new Vector2(startPosition.x, startPosition.y + Random.Range(0.5f, 4.0f) * direction);
        return new Vector2(0, 1);
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

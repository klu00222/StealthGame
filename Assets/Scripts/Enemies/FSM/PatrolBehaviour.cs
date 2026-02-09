using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float stayTime = 3.0f;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float obstacleDistance = 0.2f;

    private float timer;
    private Transform player;
    private Transform hitDetection;
    private bool playerClose;

    public LayerMask Obstacles;

    private void OnEnable()
    {
        VisionDetector.OnPlayerDetected += IsPlayerClose;
    }

    private void OnDisable()
    {
        VisionDetector.OnPlayerDetected -= IsPlayerClose;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        playerClose = false;
        hitDetection = animator.transform.Find("HitDetector");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool timeUp = IsTimeUp();

        animator.SetBool("isChasing", playerClose);
        animator.SetBool("isPatrolling", !timeUp);

        animator.transform.Translate(speed * Time.deltaTime * animator.transform.right, Space.World);

        if (ObstacleDetected(animator.transform))
        {
            Flip(animator.transform);
        }
    }

    private void IsPlayerClose()
    {
        playerClose = true;
    }

    private bool ObstacleDetected(Transform transform)
    {
        RaycastHit2D hit = Physics2D.Raycast(hitDetection.position, transform.right, obstacleDistance, Obstacles);
        return (hit.collider != null);
    }

    private void Flip(Transform transform)
    {
        transform.Rotate(0, 180, 0);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return (timer > stayTime);
    }
}

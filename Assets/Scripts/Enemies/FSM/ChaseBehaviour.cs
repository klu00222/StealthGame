using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    public float VisionRange = 4.0f;

    private Transform player;
    private bool playerClose;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("isChasing", playerClose);

        Vector2 direction = player.position - animator.transform.position;
        animator.transform.position += speed * Time.deltaTime * (Vector3)direction.normalized;
    }

    private void IsPlayerClose()
    {
        playerClose = true;
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, player.position);
        return (dist < VisionRange);
    }
}

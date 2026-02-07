using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    private Transform player;

    public float VisionRange;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("Chase", playerClose);

        Vector2 direction = player.position - animator.transform.position;
        animator.transform.position += speed * Time.deltaTime * (Vector3)direction.normalized;
    }

    private bool IsPlayerClose(Transform transform)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return (distance < VisionRange);
    }
}

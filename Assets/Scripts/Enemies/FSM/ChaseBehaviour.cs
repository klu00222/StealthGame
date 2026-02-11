using System;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    private EnemyData data;
    private Transform player;
    private Rigidbody2D rb;

    [SerializeField] private float chaseSpeedMult = 3.0f;

    private static readonly int IsChasingHash = Animator.StringToHash("isChasing");
    private static readonly int IsPatrollingHash = Animator.StringToHash("isPatrolling");

    public static event Action<bool> OnChasingChange;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        data = animator.GetComponent<EnemyData>();
        rb = animator.GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        OnChasingChange?.Invoke(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnChasingChange?.Invoke(false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || data == null || rb == null)
        {
            return;
        }

        bool canSeePlayer = data.CanSeePlayer();

        animator.SetBool(IsChasingHash, canSeePlayer);
        animator.SetBool(IsPatrollingHash, !canSeePlayer);

        if (canSeePlayer)
        {
            Vector2 currentPosition = rb.position;
            Vector2 targetPosition = player.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;


            if (direction != Vector2.zero)
            {
                //Handle rotation
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetAngle, data.RotationSpeed * Time.deltaTime));
            }

            //Move towards player
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, data.Speed * chaseSpeedMult * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }
}

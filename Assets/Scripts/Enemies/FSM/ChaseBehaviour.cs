using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{

    private EnemyData data;
    private Transform player;
    private static readonly int IsChasingHash = Animator.StringToHash("isChasing");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layer)
    {
        data = animator.GetComponent<EnemyData>();
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || data == null)
        {
            return;
        }

        bool canSeePlayer = data.CanSeePlayer();

        animator.SetBool(IsChasingHash, canSeePlayer);

        if (canSeePlayer)
        {
            Transform enemyTransform = animator.transform;
            Vector3 direction = player.position - enemyTransform.position;


            if (direction != Vector3.zero)
            {
                //Handle rotation
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);


                enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, rotation, data.rotationSpeed * Time.deltaTime);


            }

            //Move towards player
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, player.position, data.speed * Time.deltaTime);
        }
    }
}

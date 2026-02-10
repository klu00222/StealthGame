using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class EnemyChase : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float visionRange = 4.0f;
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private Transform spriteGameObject;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsPlayerClose())
        {
            animator.SetBool("isPatrolling", false);

            Vector3 direction = player.position - transform.position;

            // Rotate enemy toward player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            //Reset visuals gameobject's position and rotation
            spriteGameObject.localPosition = Vector3.zero;
            spriteGameObject.rotation = Quaternion.identity;

            // Move toward player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    private bool IsPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < visionRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Ending");
    }
}

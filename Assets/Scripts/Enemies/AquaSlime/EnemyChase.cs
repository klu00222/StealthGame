using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float visionRange = 4.0f;
    [SerializeField]
    private float speed = 2.0f;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsPlayerClose())
        {
            animator.SetBool("Patrol", false);

            Vector2 direction = player.position - transform.position;
            transform.position += speed * Time.deltaTime * (Vector3)direction.normalized;
        }
        else
        {
            animator.SetBool("Patrol", true);
        }
    }

    private bool IsPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return (distance < visionRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Ending");
    }
}

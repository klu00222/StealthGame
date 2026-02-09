using UnityEngine;

public class EnemyFreeMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    public Transform HitDetection;
    public LayerMask Obstacles;

    private void Update()
    {
        Move();

        if (ObstacleDetected())
        {
            Debug.Log("Obstacle detected");
            Flip();
        }
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * transform.right, Space.World);
    }

    private bool ObstacleDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(HitDetection.position, Vector2.right, Obstacles);
        return (hit.collider != null);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}

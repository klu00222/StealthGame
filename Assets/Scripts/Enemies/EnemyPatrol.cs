using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform Waypoints;

    public Transform ObstaclePoint;
    public LayerMask Ground;

    public static event Action OnEdgeDetected;

    private bool collided;
    private bool coolDown;

    private void Update()
    {
        if ((EdgeDetected() || collided) && !coolDown)
        {
            Debug.Log("Edge detected");
            OnEdgeDetected?.Invoke();

            collided = false;
            StartCoroutine(CoolDown());
        }

    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(ObstaclePoint.position, Vector2.up, 1.5f, Ground);

        if (hit.collider != null)
        {
            hit = Physics2D.Raycast(ObstaclePoint.position, Vector2.down, 1.5f, Ground);
        }

        return (hit.collider == null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
    }

    private IEnumerator CoolDown()
    {
        coolDown = false;
        yield return new WaitForSeconds(0.5f);
        coolDown = true;
    }
}

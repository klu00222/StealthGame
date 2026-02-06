using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 target;
    private Vector3 smoothedPosition;

    [SerializeField]
    private Transform player;

    [SerializeField][Range(1f, 5f)] private float smoothSpeed;

    private void Start()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, -10);

    }

    private void Update()
    {
        target.x = player.transform.position.x;
        target.y = player.transform.position.y;

        //Move a percentage of the position distance every frame
        smoothedPosition = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}

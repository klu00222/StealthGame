using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 target;

    [SerializeField]
    private Transform player;

    void Start()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    void Update()
    {
        target.x = player.transform.position.x;
        target.y = player.transform.position.y;

        transform.position = target;
    }
}

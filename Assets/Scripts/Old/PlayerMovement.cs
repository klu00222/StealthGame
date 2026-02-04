using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    Rigidbody2D rigidbody;
    private bool isMoving;

    private int score = 2000;

    public bool IsMoving => isMoving;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var moveDir = value.Get<Vector2>();

        Vector2 velocity = moveDir * Speed;
        rigidbody.linearVelocity = velocity;

        isMoving = (velocity.magnitude > 0.01f);

        if (isMoving)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // NOTE: InputSystem: "SaveScore" action becomes "OnSaveScore" method
    public void OnSaveScore()
    {
        // Usage example on how to save score
        PlayerPrefs.SetInt("Score", score);
        score = PlayerPrefs.GetInt("Score");
    }
}

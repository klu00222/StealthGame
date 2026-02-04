using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int score = 2000;

    public bool IsMoving { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        Vector2 moveDir = value.Get<Vector2>();

        Vector2 velocity = moveDir * Speed;
        rb.linearVelocity = velocity;

        IsMoving = velocity.magnitude > 0.01f;

        if (moveDir.x > 0.1f)
        {
            spriteRenderer.flipX = false; 
        }
        else if (moveDir.x < -0.1f)
        {
            spriteRenderer.flipX = true; 
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

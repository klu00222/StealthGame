using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastMoveDirection;

    //String to hash to not read strings so often
    private readonly int walking = Animator.StringToHash("Walking");
    private readonly int inputX = Animator.StringToHash("InputX");
    private readonly int inputY = Animator.StringToHash("InputY");

    private readonly int lastX = Animator.StringToHash("LastX");
    private readonly int lastY = Animator.StringToHash("LastY");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnMoveInput += HandleAnimationUpdate;
    }
    private void OnDisable()
    {
        PlayerMovement.OnMoveInput -= HandleAnimationUpdate;
    }

    private void HandleAnimationUpdate(Vector2 movement)
    {
        Debug.Log(movement);
        bool isMoving = movement.sqrMagnitude > 0;

        if (isMoving)
        {
            animator.SetBool(walking, true);
            lastMoveDirection = movement;

            animator.SetFloat(inputX, movement.x);
            animator.SetFloat(inputY, movement.y);
        }
        else
        {
            animator.SetBool(walking, false);
            animator.SetFloat(lastX, lastMoveDirection.x);
            animator.SetFloat(lastY, lastMoveDirection.y);
        }
    }
}

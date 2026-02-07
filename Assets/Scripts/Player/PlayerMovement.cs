using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private InputActionReference moveActionReference;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;

    public static event Action<Vector2> OnMoveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        OnMoveInput?.Invoke(moveInput);
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Speed * Time.fixedDeltaTime * moveInput.normalized));
    }

    public void OnMovePerformed(InputAction.CallbackContext value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        moveInput = value.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext value)
    {
        moveInput = Vector2.zero;
    }

    public void OnEnable()
    {
        moveActionReference.action.Enable();
        moveActionReference.action.performed += OnMovePerformed;
        moveActionReference.action.canceled += OnMoveCanceled;
    }

    public void OnDisable()
    {
        moveActionReference.action.performed -= OnMovePerformed;
        moveActionReference.action.canceled -= OnMoveCanceled;
        moveActionReference.action.Disable();
    }
}

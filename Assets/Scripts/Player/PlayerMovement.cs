using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private InputActionReference moveActionReference;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    public static event Action<Vector2> OnMoveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnMoveInput?.Invoke(moveInput);
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * moveInput.normalized));
    }

    public void OnMovePerformed(InputAction.CallbackContext value)
    {
        //Read value from control
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

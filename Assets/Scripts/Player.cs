using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Weapon weapon;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Rotaci�n con mouse")]
    public Camera mainCamera;
    public LayerMask groundMask;

    [Header("Salto")]
    public float jumpForce = 5f;
    private bool isJumping = false;

    [Header("Agacharse")]
    public float crouchScale = 0.5f;
    private Vector3 originalScale;
    private bool isCrouching = false;

    private Rigidbody rb;
    private Animator animator; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        animator = GetComponent<Animator>(); 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isCrouching)
        {
            isJumping = true;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScale, originalScale.z);
        }
        else if (context.canceled)
        {
            isCrouching = false;
            transform.localScale = originalScale;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {


        if (context.performed)
        {
            weapon.Shoot();
        }
    }

    void Update()
    {
       
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            Vector3 lookPoint = hit.point;
            lookPoint.y = transform.position.y;

            Vector3 direction = lookPoint - transform.position;

            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;

        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
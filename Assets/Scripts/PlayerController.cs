using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody rb;

    [Header("Rotacion con mouse")]
    public Camera mainCamera;
    public LayerMask groundMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // Rotar hacia el mouse
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
        
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.linearVelocity = moveDirection * moveSpeed + new Vector3(0f, rb.linearVelocity.y, 0f);
    }
}
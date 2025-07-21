using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Weapon weapon;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float flashSpeed = 15f;
    private float currentSpeed;
    private Vector2 moveInput;

    [Header("Rotación con mouse")]
    public Camera mainCamera;
    public LayerMask groundMask;

    [Header("Salto")]
    public float jumpForce = 5f;
    private bool isJumping = false;

    [Header("Agacharse")]
    public float crouchScale = 0.5f;
    private Vector3 originalScale;
    private bool isCrouching = false;

    [Header("Flash Mode")]
    public KeyCode flashKey = KeyCode.LeftShift;
    private bool inFlashTime = false;

    [Header("Audios")]
    public AudioSource musicaFondo;   // Música de fondo
    public AudioSource musicaFlash;   // Música de "Quicksilver"
    public AudioSource audioFx;       // Efecto al entrar en Flash

    private Rigidbody rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        currentSpeed = moveSpeed;
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
        
        if (Input.GetKeyDown(flashKey))
        {
            currentSpeed = flashSpeed;
            ActivarFlashTime();
        }

        
        if (Input.GetKeyUp(flashKey))
        {
            currentSpeed = moveSpeed;
            DesactivarFlashTime();
        }

        
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

    [System.Obsolete]
    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * currentSpeed;
        move.y = rb.velocity.y;
        rb.velocity = move;

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

    void ActivarFlashTime()
    {
        if (inFlashTime) return;

        inFlashTime = true;
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (musicaFondo != null && musicaFondo.isPlaying)
            musicaFondo.Pause();

        if (musicaFlash != null)
        {
            if (musicaFlash.isPlaying)
                musicaFlash.UnPause();
            else
                musicaFlash.Play();
        }

        if (audioFx != null)
            audioFx.Play();
    }

    void DesactivarFlashTime()
    {
        inFlashTime = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (musicaFlash != null && musicaFlash.isPlaying)
            musicaFlash.Pause();  

        if (musicaFondo != null)
            musicaFondo.UnPause();
    }

}

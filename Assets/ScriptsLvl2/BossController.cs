using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("Movimiento")]
    public float initialMoveSpeed = 30f;
    public float horizontalSpeed = 20f;
    private bool reachedTargetPosition = false;
    private Vector3 targetPosition = new Vector3(0f, 37f, 193.9f);
    private bool movingRight = true;
    private float leftLimit = -107.3f;
    private float rightLimit = 107.8f;

    [Header("Vida")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public BossHealthBarUI bossHealthUI;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public float fireRate = 1.5f;
    private float fireTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        // NO llames bossHealthUI.Show() acá
    }


    void Update()
    {
        if (!reachedTargetPosition)
        {
            MoveToTargetPosition();
        }
        else
        {
            MoveHorizontally();
            HandleShooting();
        }
    }

    void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, initialMoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            reachedTargetPosition = true;
        }
    }

    void MoveHorizontally()
    {
        float moveDirection = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * moveDirection * horizontalSpeed * Time.deltaTime, Space.World);

        if (transform.position.x >= rightLimit)
            movingRight = false;
        else if (transform.position.x <= leftLimit)
            movingRight = true;
    }

    void HandleShooting()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        if (firePoint1) Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        if (firePoint2) Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        if (firePoint3) Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        if (firePoint4) Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (bossHealthUI != null)
            bossHealthUI.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        if (bossHealthUI != null)
            bossHealthUI.Hide();

        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Victoria");
    }
}

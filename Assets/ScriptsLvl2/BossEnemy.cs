using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 15f;
    public float speed = 3f;

    public int maxHealth = 50;
    private int currentHealth;

    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float fireRate = 2f;
    private float nextFireTime;

    public float zigzagFrequency = 2f;
    public float zigzagAmplitude = 1.5f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector3 targetPosition = player.position;

           
            Vector3 zigzagOffset = transform.right * Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
            targetPosition += zigzagOffset;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

       
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void Shoot()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(bulletPrefab, point.position, point.rotation);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Victory");
    }
}

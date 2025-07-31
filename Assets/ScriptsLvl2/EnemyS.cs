using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public float speed = 3f;
    public int health = 3;
    public GameObject powerUpPrefab;
    public GameObject bulletPrefab;         
    public Transform firePoint;            
    public float minFireDelay = 1f;
    public float maxFireDelay = 3f;

    private float xMoveSpeed;
    private float xMoveDirection;
    private float changeDirTime = 2f;
    private float dirTimer;
    private float fireTimer;

    void Start()
    {
        PickNewDirection();
        ScheduleNextShot();
    }

    void Update()
    {
        
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

       
        transform.Translate(Vector3.right * xMoveDirection * xMoveSpeed * Time.deltaTime, Space.World);

        dirTimer += Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (dirTimer >= changeDirTime)
            PickNewDirection();

        if (fireTimer <= 0)
        {
            Shoot();
            ScheduleNextShot();
        }
    }

    void PickNewDirection()
    {
        xMoveSpeed = Random.Range(15f, 40f);
        xMoveDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        changeDirTime = Random.Range(1f, 3f);
        dirTimer = 0;
    }

    void ScheduleNextShot()
    {
        fireTimer = Random.Range(minFireDelay, maxFireDelay);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DropPowerUp();
            GameObject spawner = GameObject.Find("EnemySpawner");
            if (spawner != null)
            {
                spawner.GetComponent<EnemySpawner>().EnemyKilled();
            }
            Destroy(gameObject);

        }
    }

    void DropPowerUp()
    {
        if (powerUpPrefab != null && Random.value < 0.5f)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Muerte2");
        }
    }
}

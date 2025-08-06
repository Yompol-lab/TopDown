using UnityEngine;

public class bulletPrefab : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 20f;
    public float damage = 10f; // Daño configurable desde el Inspector

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Esto es opcional si ya usás Rigidbody
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Daño a enemigos comunes
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        // Daño al jefe
        BossController boss = collision.gameObject.GetComponent<BossController>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        // Destruir la bala en cualquier caso
        Destroy(gameObject);
    }
}

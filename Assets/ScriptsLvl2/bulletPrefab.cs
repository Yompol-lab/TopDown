using UnityEngine;

public class bulletPrefab : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 20f;
    public float damage = 10f; // Da�o configurable desde el Inspector

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Esto es opcional si ya us�s Rigidbody, pero lo dejamos si quer�s doble control
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificamos si el objeto tiene el componente EnemyHealth
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject); // La bala se destruye en cualquier caso al colisionar
    }
}

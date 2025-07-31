using UnityEngine;

public class EnemyBulletS : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth stats = other.GetComponent<PlayerHealth>();
            if (stats != null)
            {
                stats.RecibirDaño(damage);
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy")) // para no explotar con otros enemigos
        {
            Destroy(gameObject);
        }
    }
}

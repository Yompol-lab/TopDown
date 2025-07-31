using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject prefabAlMorir;

    private GameManager2 gameManager2;

    [System.Obsolete]
    void Start()
    {
        currentHealth = maxHealth;
        gameManager2 = GameObject.FindObjectOfType<GameManager2>(); 
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (prefabAlMorir != null)
        {
            GameObject objetoMuerte = Instantiate(prefabAlMorir, transform.position, Quaternion.identity);

            Transform bloquePadre = transform.parent;
        }

        if (gameManager2 != null) 
        {
            gameManager2.EnemyDefeated(); 
        }

        Destroy(gameObject);
    }
}

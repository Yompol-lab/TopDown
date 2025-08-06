using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public int enemiesToDefeat = 10;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public BossHealthBarUI bossHealthUI; // <--- ARRÁSTRALO DESDE LA JERARQUÍA

    public AudioSource gameMusic;      // Música de fondo normal
    public AudioSource bossMusic;      // Música del jefe

    private int enemiesDefeated = 0;
    private bool bossSpawned = false;

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if (!bossSpawned && enemiesDefeated >= enemiesToDefeat)
            SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            GameObject bossObj = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.LookRotation(Vector3.forward));
            BossController bossController = bossObj.GetComponent<BossController>();

            if (bossController != null && bossHealthUI != null)
            {
                bossController.bossHealthUI = bossHealthUI;
                bossHealthUI.Show();
                bossHealthUI.SetHealth(bossController.maxHealth, bossController.maxHealth);
            }
            else
            {
                Debug.LogWarning("No se encontró BossHealthBarUI en la escena o el jefe no tiene BossController.");
            }

            bossSpawned = true;
            if (gameMusic != null) gameMusic.Stop();
            if (bossMusic != null) bossMusic.Play();
        }
        else
        {
            Debug.LogWarning("Boss Prefab o SpawnPoint no asignado en GameManager");
        }
    }
}

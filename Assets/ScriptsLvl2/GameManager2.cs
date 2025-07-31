using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public int enemiesToDefeat = 10;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    private int enemiesDefeated = 0;
    private bool bossSpawned = false;

    public void EnemyDefeated()
    {
        enemiesDefeated++;

        if (!bossSpawned && enemiesDefeated >= enemiesToDefeat)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            bossSpawned = true;
        }
        else
        {
            Debug.LogWarning("Boss Prefab o SpawnPoint no asignado en GameManager");
        }
    }
}

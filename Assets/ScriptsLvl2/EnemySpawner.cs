using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    public Transform[] spawnPoints; 
    public float spawnInterval = 2f;

    private float timer;
    private int enemiesKilled = 0;
    private bool bossSpawned = false;

    void Update()
    {
        if (bossSpawned) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.Euler(0, 180f, 0));

    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= 10 && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        Instantiate(bossPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 180f, 0));
    }
}

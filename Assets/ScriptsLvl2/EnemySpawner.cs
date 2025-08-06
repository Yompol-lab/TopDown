using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
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

    [System.Obsolete]
    public void EnemyKilled()
    {
        GameManager2 gm = FindObjectOfType<GameManager2>();
        if (gm != null)
        {
            gm.EnemyDefeated();
        }
    }
}

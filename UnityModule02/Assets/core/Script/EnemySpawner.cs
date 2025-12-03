using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;
    public Transform TargetPoint;
    public float SpawnInterval = 2f;
    private float timer;
    public float MoveSpeed = 2f;
    public bool spawnerOn = true;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnInterval && spawnerOn)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemy.TargetPoint = TargetPoint;
    }

    public void SetSpawnerOn(bool on)
    {
        spawnerOn = on;
        if (!on){
            timer = 0f;
            DestroyAllEnemies();
            Debug.Log("Game Over !");
        }
    }

    public void DestroyAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
    
}

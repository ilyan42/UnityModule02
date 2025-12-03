using UnityEngine;

public class Base : MonoBehaviour
{
    public int Health = 5;
    public EnemySpawner enemySpawner;

    void Start()
    {
        Debug.Log("base health: " + Health );
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();   
        }
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            if (enemySpawner != null)
            {
                enemySpawner.SetSpawnerOn(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.attachedRigidbody != null ? collision.attachedRigidbody.gameObject : collision.gameObject;
        if (other.CompareTag("Enemy"))
        {
            Damage(1);
            Debug.Log("Health base: " + Health);
            Destroy(other);
        }
    }
}

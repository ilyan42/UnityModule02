using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public Transform TargetPoint;
    public float health = 3f;

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (TargetPoint != null)
        {
            Vector3 direction = (TargetPoint.position - transform.position).normalized;
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Enemy health: " + health);
    }
}
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float damage = 1f;
    public float destroyAfterSeconds = 5f;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    void Update()
    {
        if (target == null)
        {
            // cible déjà détruite → désintégrer la balle
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        float step = speed * Time.deltaTime;
        transform.position += dir * step;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        var enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
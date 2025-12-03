// ...existing code...
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public float range = 50f;
    public float fireRate = 0.5f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletDamage = 1f;

    // cibles actuellement dans le trigger
    private readonly List<Transform> targetsInRange = new List<Transform>();

    void Update()
    {
        if (fireCountdown > 0f)
            fireCountdown -= Time.deltaTime;

        if (targetsInRange.Count == 0) return;

        Transform nearest = GetNearestTarget();
        if (nearest != null && fireCountdown <= 0f)
        {
            Shoot(nearest);
            fireCountdown = 1f / fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.attachedRigidbody != null ? collision.attachedRigidbody.gameObject : collision.gameObject;
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.attachedRigidbody != null ? collision.attachedRigidbody.gameObject : collision.gameObject;
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
        }
    }

    Transform GetNearestTarget()
    {
        Transform best = null;
        float bestDistSqr = range * range;
        Vector3 pos = transform.position;

        // Nettoyage des cibles null détruites
        for (int i = targetsInRange.Count - 1; i >= 0; i--)
        {
            if (targetsInRange[i] == null)
                targetsInRange.RemoveAt(i);
        }

        foreach (Transform t in targetsInRange)
        {
            if (t == null) continue;
            float d2 = (t.position - pos).sqrMagnitude;
            if (d2 <= bestDistSqr)
            {
                best = t;
                bestDistSqr = d2;
            }
        }
        return best;
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null || firePoint == null || target == null) return;
        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // adapte le nom de la classe du script projectile si besoin
        var proj = b.GetComponent<projectile>();
        if (proj != null)
        {
            proj.SetTarget(target);
            proj.damage = bulletDamage;
        }
    }
}
// ...existing code...
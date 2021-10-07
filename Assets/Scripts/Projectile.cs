using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float aliveTime;
    public float damage;

    float spawnTime;
    Vector3 velocity;
    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if (spawnTime + aliveTime < Time.time)
            Destroy(gameObject);

        velocity = transform.forward * speed * Time.deltaTime;
        transform.position += velocity;
        /*
        Ray ray = new Ray(transform.position + transform.forward * transform.localScale.z / 2, transform.forward);
        RaycastHit info;

        if (Physics.Raycast(ray, out info, velocity.magnitude))
        {
            if (info.collider.tag == "Enemy")
                Destroy(info.transform.gameObject);
            Destroy(gameObject);
        }
        */

        Collider[] hit = Physics.OverlapBox(transform.position + transform.forward * transform.localScale.z / 2,
            new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, velocity.magnitude / 2));

        Damageable d;
        bool destroy = false;
        foreach (Collider c in hit)
            if((d = c.GetComponent<Damageable>()) != null)
            {
                Vector3 direction = (transform.position - c.transform.position).normalized;
                d.TakeDamage(damage, direction);
                destroy = true;
                break;
            }

        if (destroy)
            Destroy(gameObject);
    }
}

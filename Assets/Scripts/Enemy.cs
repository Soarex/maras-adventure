using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Damageable)),
    RequireComponent(typeof(UnityEngine.AI.NavMeshAgent)),
    RequireComponent(typeof(Animator))
]
public class Enemy : MonoBehaviour
{
    public new GameObject particleSystem;
    public ParticleSystem hitParticle;
    public int id = 5;

    public float minDistance;
    public float knockbackForce = 2f;
    public float knockbackTime = 1f;
    public float shootSpeed = 1;

    public Transform projectileSpawn;
    public GameObject projectile;

    Animator animator;
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    Player player;
    bool knockbackEnded = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pathfinder = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        GetComponentInChildren<Damageable>().OnHit += OnHit;
        GetComponentInChildren<Damageable>().OnDestruction += OnDestruction;
        player = FindObjectOfType<Player>();
        target = player.transform;
        animator.SetFloat("Knockback Speed Multiplier", knockbackTime);
        StartCoroutine("FindPlayer");
        StartCoroutine("Shoot");
    }

    Vector3 targetPosition;
    void Update()
    {
        targetPosition = new Vector3(target.position.x, 0, target.position.z);
        transform.LookAt(targetPosition);
    }

    IEnumerator FindPlayer()
    {
        float refreshRate = .25f;
        float sqrLen = minDistance + 2;

        while (target != null)
        {
            if (sqrLen < minDistance)
            {
                pathfinder.SetDestination(targetPosition);
            }

            Vector3 offset = target.position - transform.position;
            sqrLen = offset.sqrMagnitude;
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator Shoot()
    {
        float sqrLen = minDistance + 2;

        while (target != null)
        {
            if (sqrLen < minDistance)
            {
                GameObject p = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
                p.transform.LookAt(new Vector3(target.position.x, 0.86f, target.position.z));
            }

            Vector3 offset = target.position - transform.position;
            sqrLen = offset.sqrMagnitude;
            yield return new WaitForSeconds(shootSpeed);
        }
    }

    void OnHit(Vector3 direction)
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        hitParticle.Stop();
        hitParticle.Play();
        animator.SetTrigger("Knockback");
        knockbackEnded = false;
        StartCoroutine(Knockback());
    }

    void OnDestruction()
    {
        Instantiate(particleSystem, transform.position, Quaternion.identity);
        Status.remaining[id]--;
    }

    IEnumerator Knockback()
    {
        while (!knockbackEnded)
        {
            transform.position += transform.forward * -1 * knockbackForce * Time.deltaTime;
            yield return null;
        }
    }

    void EndKnockback()
    {
        knockbackEnded = true;
    }
}

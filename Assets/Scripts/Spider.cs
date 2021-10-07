using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Damageable)),
    RequireComponent(typeof(Animator))
]
public class Spider : MonoBehaviour
{
    public new GameObject particleSystem;
    public ParticleSystem hitParticle;

    public float speed = 1;
    public float timeBetweenMovements = 3;
    public float moveDistance;
    public float knockbackForce = 2f;
    public float knockbackTime = 1f;

    Animator animator;
    Player player;
    float nextMoveTime = 0;
    float distanceWalked = 0;
    bool isMoving = false;
    bool knockbackEnded = false;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        GetComponentInChildren<Damageable>().OnHit += OnHit;
        GetComponentInChildren<Damageable>().OnDestruction += OnDestruction;
        player = FindObjectOfType<Player>();
        animator.SetFloat("Knockback Speed Multiplier", knockbackTime);
    }

    void Update()
    {
        if(Time.time > nextMoveTime && !isMoving)
        {  
            distanceWalked = 0;
            transform.localEulerAngles = Vector3.up * Random.Range(0, 360);
            animator.SetBool("IsRunning", true);
            isMoving = true;
            StartCoroutine("Walk");
        }
    }

    IEnumerator Walk()
    {
        while (distanceWalked < moveDistance)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            distanceWalked += speed * Time.deltaTime;
            yield return null;
        }
        
        nextMoveTime = Time.time + timeBetweenMovements;
        animator.SetBool("IsRunning", false);
        isMoving = false;
        
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

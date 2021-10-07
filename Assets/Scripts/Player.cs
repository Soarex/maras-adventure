using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Damageable)), 
    RequireComponent(typeof(Animator)), 
    RequireComponent(typeof(CharacterController))
]
public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float slideFriction = 0.3f;
    public float gravityMultiplier = 0.2f;
    public float speedSmoothTime = 0.2f;
    public float turnSmoothTime = 0.2f;

    public Transform cameraTransform;
    public Joystick joystick;

    Vector3 cameraOffset;
    Vector3 velocity;
    Vector3 hitNormal;
    float turnSmoothVelocity;

    Animator animator;
    CharacterController controller;
    void Start()
    {
        Status.Init();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cameraOffset = cameraTransform.position - transform.position;
        animator.SetLayerWeight(1, 1);
    }

    void Update()
    {
        float temp = velocity.y;

#if TEST
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
#else
        if (!Status.inDialogue)
            velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized * speed;
        else
            velocity = Vector3.zero;

#endif

        if (velocity.magnitude != 0)
        {
            animator.SetFloat("Speed Percent", 1, speedSmoothTime, Time.deltaTime);

#if TEST
            float angle = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;    
#else
            float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
#endif

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
        }
        else
            animator.SetFloat("Speed Percent", 0, speedSmoothTime, Time.deltaTime);

        velocity.y = temp;

#if TEST
        if (Input.GetButtonDown("Fire1"))
            Shoot();
#endif

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = jumpForce;

        if(!controller.isGrounded)
            velocity.y += Physics.gravity.y * gravityMultiplier;

        controller.Move(velocity * Time.deltaTime);
        cameraTransform.position = transform.position + cameraOffset;
        
    }
    private void FixedUpdate()
    {
        if (velocity.y < 0)
        {
            Collider[] colliders = Physics.OverlapBox(
                transform.position - Vector3.up * controller.bounds.size.y / 2f,
                new Vector3(controller.bounds.size.x / 4f, 0.01f, controller.bounds.size.z / 4f)
            );

            foreach (Collider c in colliders)
                if (c.tag == "Enemy")
                {
                    Destroy(c.gameObject);
                    velocity.y = jumpForce;
                }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<Damageable>().TakeDamage(2, Vector3.zero);
            
        }
    }

    void OnHit(Vector3 direction)
    {
        animator.SetTrigger("Damage");
    }

    public void Shoot()
    {
        animator.SetTrigger("Attack");
    }

    public void AttackFinished()
    {
        
    }
}

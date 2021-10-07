using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Sword : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Vector3 direction = (-transform.position + other.transform.position).normalized;
            Damageable damageable;
            if ((damageable = other.gameObject.GetComponent<Damageable>()) != null)
                damageable.TakeDamage(2, transform.parent.parent.parent.parent.forward);

            CameraShaker.Instance.ShakeOnce(2, 5, 0, 1);
        }
    }
}

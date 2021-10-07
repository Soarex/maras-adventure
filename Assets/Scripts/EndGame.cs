using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Animator animator;

    void OnDestroy()
    {
        animator.SetTrigger("End");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int id;

    void Start()
    {
        
    }

    void Update()
    {
        if (Status.remaining[id] == 0)
            Destroy(gameObject);
    }
}

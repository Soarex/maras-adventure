using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEvents : MonoBehaviour
{
    public GameObject toDisable;

    void Start()
    {
        if (Status.wolfFreed)
            toDisable.SetActive(false);
    }
}

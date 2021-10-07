using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUpdate : MonoBehaviour
{
    public int id;

    void Start()
    {
        Status.prevArea = id;
    }
}

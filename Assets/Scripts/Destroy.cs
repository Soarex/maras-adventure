using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnDestroy()
    {
        Status.remaining[3] = 0;   
    }
}

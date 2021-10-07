using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaClear : MonoBehaviour
{ 
    void OnDestroy()
    {
        Status.startAreaCleared = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTeleporter : MonoBehaviour
{
    public int sceneIndex;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

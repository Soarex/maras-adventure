using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaEvents : MonoBehaviour
{
    public GameObject toDisable;
    public Transform player;
    public Transform camera;
    public Transform startPosition;

    void Start()
    {
        Vector3 cameraOffset = camera.transform.position - player.transform.position;
        if (Status.startAreaCleared)
        {
            player.position = startPosition.position;
            camera.position = startPosition.position + cameraOffset;
            player.eulerAngles = startPosition.eulerAngles;
            toDisable.SetActive(false);
        }
    }
}

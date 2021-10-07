using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageEvents : MonoBehaviour
{
    public GameObject pulcino;
    public Transform player;
    public Transform camera;
    public Transform fromStartingArea;
    public Transform fromForest;

    void Start()
    {
        Vector3 cameraOffset = camera.transform.position - player.transform.position;
        if (Status.prevArea == 0)
        {
            player.position = fromStartingArea.position;
            camera.position = fromStartingArea.position + cameraOffset;
            player.eulerAngles = fromStartingArea.eulerAngles;
        }

        if (Status.prevArea == 1)
        {
            player.position = fromForest.position;
            camera.position = fromForest.position + cameraOffset;
            player.eulerAngles = fromForest.eulerAngles;
        }

        if (Status.wolfFreed && !Status.pulcinoVillage)
            pulcino.SetActive(true);
    }
}

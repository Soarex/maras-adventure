using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactionRange = 2f;

    public GameObject interactionButton;
    public Transform interactionButtonPosition;

    bool playerIsClose;
    GameObject player;
    GameObject buttonInstance;
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    { 
        if (playerIsClose)
        {
            if(buttonInstance == null)
            {
                buttonInstance = Instantiate(interactionButton);
                buttonInstance.transform.position = interactionButtonPosition.position;
            }

            if (Input.GetButtonDown("Fire1"))
                 GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            if (buttonInstance != null)
                Destroy(buttonInstance);
        }
    }

    private void FixedUpdate()
    {
        playerIsClose = CheckForPlayer();
    }

    bool CheckForPlayer()
    {   
        if (Vector3.Distance(player.transform.position, transform.position) < interactionRange)
            return true;
        else
            return false;
    }
}

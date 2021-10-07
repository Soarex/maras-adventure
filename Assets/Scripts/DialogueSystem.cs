using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject uiControls;
    public GameObject textBox;
    public TextMeshProUGUI title;
    public TextMeshProUGUI text;

    int currentSlot;
    bool thisDialogue;

    private void Start()
    {
        thisDialogue = false;
        currentSlot = 0;
        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && thisDialogue == true)
                NextLine();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Sword")
            return;
        StartDialogue();
    }

    void StartDialogue()
    {
        Status.inDialogue = true;
        thisDialogue = true;
        uiControls.SetActive(false);
        textBox.SetActive(true);
        NextLine();
    }

    void EndDialogue()
    {
        Status.inDialogue = false;
        thisDialogue = false;
        uiControls.SetActive(true);
        textBox.SetActive(false);
        currentSlot = 0;
        Destroy(gameObject);
    }

    public void NextLine()
    {
        if(currentSlot >= dialogue.dialogues.Length)
        {
            EndDialogue();
            return;
        }
        title.text = dialogue.titles[currentSlot];
        text.text = dialogue.dialogues[currentSlot];
        currentSlot++;
    }
}

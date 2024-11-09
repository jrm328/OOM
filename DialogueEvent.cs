using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{

    public DialogueTrigger dialogueTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        // Check if the object that collided has the Player tag
        if (other.CompareTag("Player"))
        {
            // Call the function from the GameManager script
            dialogueTrigger.TriggerDialogue();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object that collided has the Player tag
        if (collision.CompareTag("Player"))
        {
            // Call the function from the GameManager script
            dialogueTrigger.TriggerDialogueEnd();
        }
    }
}

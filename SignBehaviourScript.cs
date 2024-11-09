using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehaviourScript : MonoBehaviour, IInteractable
{
    public DialogueTrigger dialogueTrigger;

    private bool isBlinking = false;
    [SerializeField] private string message;
    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private Color startColor = Color.white; // Starting color (white)
    private Color targetColor = Color.red;  // Target color (red)
    private float blinkSpeed = 2f;          // Speed of the blinking effect

    private void Start()
    {
        spriteRenderer.color = startColor;  // Ensure we start with the default color
    }

    public void Interact()
    {
        ShowMessage();
    }

    public void ShowVisualCue()
    {
        isBlinking = true; // Start blinking
    }

    public void HideVisualCue()
    {
        isBlinking = false; // Stop blinking
        spriteRenderer.color = startColor; // Reset the color to white when the player moves away
        dialogueTrigger.TriggerDialogueEnd(); // Remove the Dialogue when no longer interacting
    }

    private void Update()
    {
        if (isBlinking)
        {
            // Calculate the color interpolation based on time
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
        }
    }

    private void ShowMessage()
    {
        // Show the message on the UI
        //Debug.Log("Sign reads: " + message);
        dialogueTrigger.TriggerDialogue();

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private Image uiImage;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float typeSpeed = 0.1f;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " +  dialogue.name);

        nameText.text = dialogue.name;

        uiImage.sprite = dialogue.speakerImage;

        sentences.Clear();

        animator.SetBool("IsOpen", true);



        foreach (string sentence in dialogue.sentences) 
        { 
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    
    public Text nametext;
    public Text dialogueText;
    public Image dialogueBox;

    public Animator animator;
    

    private Queue<string> sentences;

	// Use this for initialization
	void Start ()
    {
        sentences = new Queue<string>();
        nametext.enabled = false;
        dialogueText.enabled = false;
        dialogueBox.enabled = false;
        animator.SetBool("isUse", false);
        

    }

    public void StartDialogue(Dialogue dialogue, int textDelay)
    {
        nametext.text = dialogue.name;

        sentences.Clear();

        nametext.enabled = true;
        dialogueText.enabled = true;
        dialogueBox.enabled = true;
        animator.SetBool("isUse", true);
        

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Invoke("EndDialogue", textDelay);

        //DisplayNextSentence();

        string sentencing = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentencing));
    }

    public void StopDialogue(int textDelay)
    {
        Invoke("EndDialogue", textDelay);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        //Debug.Log("End of conversation.");
        nametext.enabled = false;
        dialogueText.enabled = false;
        dialogueBox.enabled = false;
        animator.SetBool("isUse", false);
        
    }
}

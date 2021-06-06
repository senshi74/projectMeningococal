using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NurseInteract : MonoBehaviour {
    public AudioSource doorSound;
    bool interacting = false;
    public PlayerScript playerScript;
    public Dialogue dialogue;
    [SerializeField] private int textDelay;

    [Header("Nurse Dialogue")]
    public GameObject dialogueBox;
    public Text dialogueName;
    public Text dialogueText;

    // Use this for initialization
    void Start () {
        doorSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Checks if interact button was pressed
        if (Input.GetButtonDown("Interact"))
        {
            interacting = true;
            //Debug.Log(interacting);
        }

        if (collision.name == "Player" && interacting == true && playerScript.meningococcal == true)
        {
            doorSound.Play();
            // Shakes objective box to indicate a change
            StartCoroutine(FindObjectOfType<ObjectiveShake>().Shake(1, 1));

            // Goal resets
            collision.GetComponent<PlayerScript>().meningococcal = false;
            // Risk goes down to 50
            collision.GetComponent<PlayerScript>().currentRisk = 50f;
            // Allows player to get symptoms again
            collision.GetComponent<PlayerScript>().hasSymptom = false;
            // Gets rid of darkened screen
            collision.GetComponent<PlayerScript>().darkScreen.gameObject.SetActive(false);
            // Nurse dialogue depending on symptom
            string person;
            string text;

            switch (collision.GetComponent<PlayerScript>().symptom)
            {
                case "dizziness":
                    person = "Nurse";
                    text = "Your dizziness may not be caused by meningococcal, but if any other symptoms occur please come right back to the nurse";
                    SetDialogue(person, text);
                    break;
                case "headache":
                    person = "Nurse";
                    text = "You had a normal headache unrelated to meningococcal.\nHere is this will relieve the headache.";
                    SetDialogue(person, text);
                    break;
                default: break;
            }
            // Resets the symptom
            collision.GetComponent<PlayerScript>().symptom = "";
        }
        else if (collision.name == "Player" && interacting == true && playerScript.meningococcal == false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, textDelay);
        }

        Reset();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<DialogueManager>().StopDialogue(textDelay);
    }

    private void Reset()
    {
        interacting = false;
    }

    private void ResetDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        dialogueName.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    private void SetDialogue(string person, string text)
    {
        dialogueName.text = person;
        dialogueText.text = text;

        dialogueBox.gameObject.SetActive(true);
        dialogueName.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        Invoke("ResetDialogue", 3);
    }
}

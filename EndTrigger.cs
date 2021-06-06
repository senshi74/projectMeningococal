using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour 
{
    public AudioSource endSound;
    public PlayerScript playerScript;

    public int textDelay;
    public Dialogue dialogue;

    public bool interacting = false;

    [Header("Scene Selection")]
    public bool demoScene;
    public bool level2;
    public bool level3;

    //use this for initialization
    void Start()
    {
        endSound = GetComponent<AudioSource>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact"))
        {
            interacting = true;
        }

        if (demoScene == true)
        {
            if (playerScript.thirsty == false && interacting == true)
            {
                endSound.Play();
                FindObjectOfType<GameControl>().CompleteLevel();
                FindObjectOfType<PlayerMovement>().canMove = false;
                FindObjectOfType<TimeLeft>().canCount = false;
            }
            else if (playerScript.thirsty == true && interacting == true)
            {
                //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, textDelay);
                StartDialogue();
            }
        }

        if (level2 == true)
        {
            if (FindObjectOfType<PersistantData>().count > 0 && interacting == true)
            {
                endSound.Play();
                FindObjectOfType<GameControl>().CompleteLevel();
                FindObjectOfType<PlayerMovement>().canMove = false;
                FindObjectOfType<TimeLeft>().canCount = false;
            }
            else if (FindObjectOfType<PersistantData>().count == 0 && interacting == true)
            {
                //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, textDelay);
                StartDialogue();
            }
        }


        if (level3 == true)
        {
            if (playerScript.immunised == true && interacting == true && FindObjectOfType<PlayerScript>().meningococcal == false)
            {
                endSound.Play();
                FindObjectOfType<GameControl>().CompleteLevel();
                FindObjectOfType<PlayerMovement>().canMove = false;
                FindObjectOfType<TimeLeft>().canCount = false;
            }
            else if (playerScript.immunised == false && interacting == true)
            {
                StartDialogue();
            }
        }

        Reset();
    }

    private void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, textDelay);
    }

    private void Reset()
    {
        interacting = false;
    }
}

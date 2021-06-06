using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    [Header("Objective Box")]
    public GameObject objectiveBox;
    public Text objectiveText;

    [Header("PopUp Box")]
    public GameObject dialogueBox;
    public Text dialogueName;
    public Text dialogueText;

    [Header("Scene Selection")]
    public bool demoScene; 
    public bool level2;
    public bool level3;

    [Header("Risk Bar")]
    public float minRisk;
    public float maxRisk;
    [System.NonSerialized] public float currentRisk;
    public Slider riskBar;

    [Space(10)]
    public int maxDrinks;
    public Image darkScreen;

    [System.NonSerialized]public bool thirsty = true;
    [System.NonSerialized]public bool meningococcal = false;
    [System.NonSerialized]public bool hasSymptom = false;
    [System.NonSerialized]public string symptom = "";
    [System.NonSerialized]public bool immunised = false;

    public float duration;

    // Use this for initialization
    void Start () {
        riskBar.minValue = minRisk;
        riskBar.maxValue = maxRisk;

        if (level3 == true)
        {
            switch (FindObjectOfType<PersistantData>().count)
            {
                case 1:
                    currentRisk = 66f;
                    break;
                case 2:
                    currentRisk = 33f;
                    break;
                default:
                    currentRisk = 0f;
                    break;
            }
        }
        else
        {
            currentRisk = minRisk;
        }

        riskBar.value = currentRisk;
	}


    // Update is called once per frame
    //set bool later to determine thirsty level
    void Update () 
    {
        riskBar.value = currentRisk;

        if (demoScene == true)
        {
            objectiveBox.gameObject.SetActive(true);
            objectiveText.gameObject.SetActive(true);

            if (meningococcal == true)//Placeholder text to show that player needs to get a drink
            {
                objectiveText.text = "You show signs of Meningococcal!\nGo and see the nurse!";
            }
            else if (thirsty == true)
            {
                objectiveText.text = "You are thirsty and require a drink before class starts!";
            }
            else if (thirsty == false)//Dissable above text after player has gotten a drink
            {
                objectiveText.text = "Go to class!";
            }
        }

        if (level2 == true)
        {
            objectiveBox.gameObject.SetActive(true);
            objectiveText.gameObject.SetActive(true);

            if (meningococcal == true)//Placeholder text to show that player needs to get a drink
            {
                objectiveText.text = "You show signs of Meningococcal!\nGo and see the nurse!";
            }
            else if (FindObjectOfType<PersistantData>().count < maxDrinks)
            {
                objectiveText.text = FindObjectOfType<PersistantData>().count.ToString() + "/" + maxDrinks.ToString() + " Drinks Collected";
            }
            else if (FindObjectOfType<PersistantData>().count >= maxDrinks)
            {
                objectiveText.text = "Go back to your friends!";
            }
        }
    
        if (level3 == true)
        {
            objectiveBox.gameObject.SetActive(true);
            objectiveText.gameObject.SetActive(true);

            if (meningococcal == true)//Placeholder text to show that player needs to get a drink
            {
                objectiveText.text = "You show signs of Meningococcal!\nGo and see the nurse!";
            }
            else if (immunised == false)
            {
                objectiveText.text = "There has been an outbreak of meningococcal W! Go and see the nurse!";
            }
            else if (meningococcal == false && immunised == true)
            {
                objectiveText.text = "Go to the Exit!";
            }        
        }
    }

    private void ResetDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        dialogueName.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    private void SetDialogue(string person, string dialogue)
    {
        dialogueName.text = person;
        dialogueText.text = dialogue;

        dialogueBox.gameObject.SetActive(true);
        dialogueName.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        Invoke("ResetDialogue", 3);
    }

    public void Damage(float damage)
    {
        if (damage <= 0)//No damage taken
        {
            return;
        }

        currentRisk += damage;

        riskBar.value = currentRisk;

        if (currentRisk >= 100 && hasSymptom == false)//Risk bar is full
        { //Code to add symptyms

            // Shakes objective box to indicate a change in objective
            StartCoroutine(FindObjectOfType<ObjectiveShake>().Shake(1, 1));

            // Indicates player has a symptom
            meningococcal = true;
            // Gives the player a symptom
            GiveSymptom();
            //Makes it so that you can only get 1 symptom at a time.
            hasSymptom = true;
        }
    }

    private void GiveSymptom()
    {
        //Code that picks a meningococcal symptom.
        int rand = Random.Range(0, 2);
        switch(rand)
        {
            case 0:
                ScreenShake();
                break;
            case 1:
                DarkenScreen();
                break;
            default: break;
        }
    }

    private void ScreenShake()//Shakes the screen to imitate dizzyness
    {
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(duration, 1f));

        string person = "Nurse";
        string dialogue = "Dizziness is a common symptom of Meningococcal!";
        SetDialogue(person, dialogue);

        symptom = "dizziness";
    }

    private void DarkenScreen()//Darkens the screen to imitate a headache
    {
        darkScreen.gameObject.SetActive(true);

        string person = "Nurse";
        string dialogue = "Headaches are a common symptom of Meningococcal!";
        SetDialogue(person, dialogue);

        symptom = "headache";
    }
}

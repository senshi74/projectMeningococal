using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NurseQuestions : MonoBehaviour 
{
    public Dialogue dialogue;

    public GameObject questionBox;
    public Text nameText;
    public Text questionText;
    public Button button1;
    public Button button2;

    public string correctAns;
    public string wrongAns;

    public Queue<string> sentence;

    [System.NonSerialized] public bool answered = false;

    // Use this for initialization
    void Start () 
    {
        sentence = new Queue<string>();

        Button btn1 = button1.GetComponent<Button>();
        Button btn2 = button2.GetComponent<Button>();

        btn1.onClick.AddListener(OnClicked);
        btn2.onClick.AddListener(OnClicked);
    }
	
    public void StartQuestions()
    {
        nameText.text = dialogue.name;
        button1.GetComponentInChildren<Text>().text = correctAns;
        button2.GetComponentInChildren<Text>().text = wrongAns;

        questionBox.SetActive(true);

        sentence.Clear();

        foreach (string sentences in dialogue.sentences)
        {
            sentence.Enqueue(sentences);
        }

        string sentencing = sentence.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentencing));
    }

    IEnumerator TypeSentence(string sent)
    {
        questionText.text = "";
        foreach (char letter in sent.ToCharArray())
        {
            questionText.text += letter;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && answered == false)
        {
            StartQuestions();
            FindObjectOfType<PlayerMovement>().canMove = false;
        }
    }

    public void OnClicked()
    {
        // Shows button was clicked.
        //Debug.Log("clicked");

        var btnName = EventSystem.current.currentSelectedGameObject;

        // Gets the name of the button object.
        //Debug.Log(btnName.name);

        // Gets the text of the button.
        //Debug.Log(btnName.GetComponentInChildren<Text>().text);

        if (btnName.GetComponentInChildren<Text>().text == correctAns)
        {
            Debug.Log("CORRECT!");
            FindObjectOfType<PlayerScript>().currentRisk = 0;
        }
        else
        {
            Debug.Log("INCORRECT!");
            FindObjectOfType<PlayerScript>().currentRisk = 90;
        }

        questionBox.SetActive(false);
        answered = true;
        //FindObjectOfType<PlayerMovement>().canMove = true;

        FindObjectOfType<PlayerScript>().immunised = true;
        StartCoroutine(FindObjectOfType<ObjectiveShake>().Shake(1, 1));
    }
}

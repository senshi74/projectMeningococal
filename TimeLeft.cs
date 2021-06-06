using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour {

    [SerializeField] private Text text;    
    [SerializeField] private float mainTimer;
    public static float timeleft;
    public bool canCount = true;
    private bool doOnce = false;
    [System.NonSerialized] public bool pause = false;

    // Use this for initialization
    void Start () {
        
        timeleft = mainTimer;

        text.text = timeleft.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (timeleft >= 0 && canCount && pause == false)
        {
            timeleft -= Time.deltaTime;
            text.text = timeleft.ToString("F");
        }

        else if (pause == true)
        {
            text.text = timeleft.ToString("F");
        }

        else if (timeleft <= 0 && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timeleft = 0;
            text.text = "Time Left:" + Mathf.Round(timeleft);
        }
        text.text = timeleft.ToString("0");
    }
}

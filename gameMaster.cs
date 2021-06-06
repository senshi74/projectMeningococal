using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int drinks;
    public Text drinksText;

    void Update()
    {
        drinksText.text = (drinks + "/3 Drinks");
    }
}

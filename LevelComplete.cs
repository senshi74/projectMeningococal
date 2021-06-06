using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour 
{
    public Text completeText;

    private void Start()
    {
        string compText = "";

        switch (FindObjectOfType<PersistantData>().count)
        {
            case 1:
                compText = "Because you only got " + FindObjectOfType<PersistantData>().count + " out of the 3 drinks, you need to share with your friends.\nThis will increase your chance of catching Meningococcal, because Meningococcal is transferred through saliva.";
                break;
            case 2:
                compText = "Because you only got " + FindObjectOfType<PersistantData>().count + " out of the 3 drinks, you and 1 friend need to share a drink.\nThis will increase your chance of catching Meningococcal, because Meningococcal is transferred through saliva.";
                break;
            case 3:
                compText = "Congratulations!\nBecause you collected all 3 drinks there is no need to share any drinks with your friends.\nThis means that there is no chance of tranferring Meningococcal.";
                break;
            default:
                break;
        }

        completeText.text = compText;
    }
}

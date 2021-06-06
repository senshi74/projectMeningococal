using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInteractable : MonoBehaviour 
{
    public GameObject interact;

    private void Start()
    {
        interact.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interact.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interact.gameObject.SetActive(false);
    }
}

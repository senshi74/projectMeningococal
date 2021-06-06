using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
    public AudioSource Drinks;
    [SerializeField] private float damage;//The amount that lowers the immunity bar
    [SerializeField] private bool destroyable = false;
    [SerializeField] private bool isFountain = false;
 
    public Transform player;
    public PlayerScript playerScript;
    
    private bool interacting = false;

    public Animator animator;
    

    //Automatically false but if checked in the inspector the gameObject will be destroyed when interacted with

    // Use this for initialization
    void Start () {
        Drinks = GetComponent<AudioSource>(); 
    }
	
	// Update is called once per frame
	void Update () 
    {

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Checks if interact button was pressed
        if (Input.GetButtonDown("Interact"))
        {
            interacting = true;
            //Debug.Log(interacting);
        }

        //Interacts with object if player is thirsty
        if (collision.name == "Player" && interacting == true)
        {
            if (FindObjectOfType<PlayerScript>().demoScene == true)
            {
                // Shakes objective box to indicate a change
                StartCoroutine(FindObjectOfType<ObjectiveShake>().Shake(1, 1));
            }

            collision.GetComponent<PlayerScript>().thirsty = false;

            playerScript.Damage(damage);
            Drinks.Play();

            if (isFountain)
            {
                animator.SetTrigger("Start");
                animator.SetBool("Flow", true);
            }

            if (destroyable == true)
            {
                Destroy(gameObject);                
            }
            
        }

        //Resets values
        Reset();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Flow", false);
    }

    private void Reset()
    {
        interacting = false;
    }
}

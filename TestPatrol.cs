using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPatrol : MonoBehaviour {
    public AudioSource soundFX;
    public float speed;
    public float distance;
    public float damage;
    public Animator animator;

    private bool movingRight = true;

    public Transform groundDetection;
    public Transform wallDetection;

    public PlayerScript playerScript;

    void Start()
    {
        soundFX = GetComponent<AudioSource>();
    }

    void Update () {

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Interact"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (wallInfo.collider != null)
        {
            //Debug.Log(wallInfo.collider.tag);
            if (wallInfo.collider.tag == "Player" || wallInfo.collider.tag == "Soda")
            {
                // do nothing
            }
            else if (wallInfo.collider == true)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            soundFX.Play();
            playerScript.Damage(damage);
            animator.SetTrigger("Interact");
        }
    }
}

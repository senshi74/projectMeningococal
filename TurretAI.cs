using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    //Integers
    public int curHealth;
    public int maxHealth;

    //Floats
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    //Booleans
    public bool awake = false;
    public bool lookingRight = true;

    //References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight;
    public AudioSource sneeze;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        
    }

    void Start()
    {
        curHealth = maxHealth;
        sneeze = GetComponent<AudioSource>();
    }

    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance < wakeRange)
        {
            awake = true;
        }

        if (distance > wakeRange)
        {
            awake = false;
        }
    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (!attackingRight)
            {
                sneeze.Play();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position,
                    shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                if (shootPointLeft == true)
                {
                       bulletClone.transform.rotation = Quaternion.Euler(0, 180, 0); 
                }

                bulletTimer = 0;
            }

            if (attackingRight)
            {
                sneeze.Play();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position,
                    shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;


                bulletTimer = 0;
            }
        }
    }









}

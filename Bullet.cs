using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true){

            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerScript>().Damage(10);
            }

            Destroy(gameObject);

        }
        
    }

}

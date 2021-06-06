using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCan : MonoBehaviour
{
    public AudioSource CoinSound;

    void Start()
    {
        CoinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            FindObjectOfType<PersistantData>().count += 1;
            CoinSound.Play();
            Destroy(gameObject);
        }
    }
}

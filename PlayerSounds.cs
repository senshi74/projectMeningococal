using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioSource jumps;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            jumps.Play();
        }
    }
}

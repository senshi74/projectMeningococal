using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBGMScence : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);   
    }
}

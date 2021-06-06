using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour {

    public int count;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
}

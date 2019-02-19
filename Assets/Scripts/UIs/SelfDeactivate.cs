using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeactivate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SelfDestruct", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SelfDestruct()
    {
        gameObject.SetActive(false);
    }
}

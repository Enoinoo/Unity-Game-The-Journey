using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1To2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponentInChildren<PlayerStatus>())
            SceneManager.LoadScene("Level2");
        if (col.GetComponentInChildren<MageStatus>())
            SceneManager.LoadScene("Level2Mage");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1To2Mage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.GetComponent<PlayerStatus>() || col.collider.GetComponent<MageStatus>())
            SceneManager.LoadScene("Level2Mage");
    }
}

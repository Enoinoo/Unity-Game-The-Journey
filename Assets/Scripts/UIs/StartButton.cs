using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    public GameObject startMenu;
    public GameObject storyMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        storyMenu.SetActive(true);
        startMenu.SetActive(false);
    }
}

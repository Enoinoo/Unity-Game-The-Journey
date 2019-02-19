using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPressQ : MonoBehaviour {

    public GameObject pressQText;

    private Image energyBar;

	// Use this for initialization
	void Start () {
        energyBar = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		if(energyBar.fillAmount == 1)
        {
            pressQText.SetActive(true);
        }
        else
        {
            pressQText.SetActive(false);
        }
	}
}

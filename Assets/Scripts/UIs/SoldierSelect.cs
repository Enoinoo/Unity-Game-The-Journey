using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelect : MonoBehaviour {

    private CharacterSelect characterSelect;

	// Use this for initialization
	void Start () {
        characterSelect = GetComponentInParent<CharacterSelect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        characterSelect.isSoldier = true;

    }
}

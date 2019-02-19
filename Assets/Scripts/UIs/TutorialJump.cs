using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJump : MonoBehaviour {

    public GameObject previousText;
    public GameObject nextText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Mage" || col.gameObject.tag == "Soldier") {
            if (previousText) previousText.SetActive(false);
            nextText.SetActive(true);
            if (nextText.name == "5" )
            {
                MageStatus mage = FindObjectOfType<MageStatus>();
                PlayerStatus player = FindObjectOfType<PlayerStatus>();
                if (mage) mage.GainEnergy(100f);
                else player.GainEnergy(100f);
            }
            if(nextText.name == "6")
            {
                PlayerStatus player = FindObjectOfType<PlayerStatus>();
                if (player.health == 100) player.ApplyDamage(30f);
            }
            Destroy(gameObject);
        }
    }
}

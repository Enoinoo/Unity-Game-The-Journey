using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponent<Target>())
        {
            Target target = col.gameObject.GetComponent<Target>();
            MageStatus mageStatus = FindObjectOfType<MageStatus>();
            target.TakeDamage(.3f);
            mageStatus.HealthRegen(.3f);
        }
    }
}

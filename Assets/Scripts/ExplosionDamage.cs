using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Target>())
        {
            Target target = col.gameObject.GetComponent<Target>();
            target.TakeDamage(50f);
        }
    }
}

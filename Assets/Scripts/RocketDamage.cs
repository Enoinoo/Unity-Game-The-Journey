using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamage : MonoBehaviour {

    public float damage = 50;
    public float time = .5f;
    public GameObject explosion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.GetComponent<Target>())
        {
            Target target = col.collider.gameObject.GetComponent<Target>();
            target.TakeDamage(damage);
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            playerStatus.GainEnergy(damage / 2);
            GameObject impactGO = Instantiate(explosion, col.contacts[0].point, col.transform.rotation);
            impactGO.GetComponent<ParticleSystem>().Play();
            //Destroy(gameObject);
            Destroy(impactGO, time);
        }
        else if (col.collider.gameObject.tag == "Terrain")
        {
            GameObject impactGO = Instantiate(explosion, col.contacts[0].point, col.transform.rotation);
            impactGO.GetComponent<ParticleSystem>().Play();
            //Destroy(gameObject);
            Destroy(impactGO, time);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCylinderDamage : MonoBehaviour {

    private ParticleSystem part;
    private List<ParticleSystem.Particle> enter;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        enter = new List<ParticleSystem.Particle>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    /*
    void OnParticleCollision()
    {
        Debug.Log("hi");
    }*/

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("hi");
        if (col.gameObject.GetComponent<Target>())
        {
            Target target = col.gameObject.GetComponent<Target>();
            target.TakeDamage(1);
            target.GainEnergy(.2f);
        }
    }
    
    /*
    void OnParticleTrigger()
    {
        int numEnter = part.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            Debug.Log("Should deal damage now");
            enter[i] = p;
        }

        part.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamage : MonoBehaviour {

    public float damage = 2f;
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

    void OnParticleCollision(GameObject obj)
    {
        if (obj.GetComponent<Target>())
        {
            Target target = obj.GetComponent<Target>();
            if (target.health >= 0)
            {
                target.TakeDamage(damage);
                target.GainEnergy(.5f);
                target.SetOnFire();
            }
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

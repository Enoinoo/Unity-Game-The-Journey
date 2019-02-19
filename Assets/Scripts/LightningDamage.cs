using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour {

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject obj)
    {
        if (obj.GetComponent<Target>())
        {
            Target target = obj.GetComponent<Target>();
            MageStatus mageStatus = FindObjectOfType<MageStatus>(); 
            target.TakeDamage(2f);
            target.GainEnergy(.2f);
            target.GainMana(4f);
            mageStatus.HealthRegen(2f);
        }
    }
}

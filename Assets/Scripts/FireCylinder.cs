using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{

    public class FireCylinder: MonoBehaviour
    {
        [Tooltip("Optional audio source to play once when the script starts.")]
        public AudioSource AudioSource;

        [Tooltip("How much force to create at the center (explosion), 0 for none.")]
        public float ForceAmount;

        [Tooltip("The radius of the force, 0 for none.")]
        public float ForceRadius;

        [Tooltip("A hint to users of the script that your object is a projectile and is meant to be shot out from a person or trap, etc.")]
        public bool IsProjectile;

        [Tooltip("Particle systems that must be manually started and will not be played on start.")]
        public ParticleSystem[] ManualParticleSystems;

        private bool isStart;

        private float timer = 0;

        private MageStatus mageStatus;
        private Firing firing;

        private IEnumerator CleanupEverythingCoRoutine()
        {
            // 2 extra seconds just to make sure animation and graphics have finished ending
            yield return new WaitForSeconds(2.0f);

            GameObject.Destroy(gameObject);
        }

        private void StartParticleSystems()
        {
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (ManualParticleSystems == null || ManualParticleSystems.Length == 0 ||
                    System.Array.IndexOf(ManualParticleSystems, p) < 0)
                {
                    if (p.main.startDelay.constant == 0.0f)
                    {
                        // wait until next frame because the transform may change
                        var m = p.main;
                        var d = p.main.startDelay;
                        d.constant = 0.01f;
                        m.startDelay = d;
                    }
                    else if (firing.GetShouldWait())
                    {
                        var m = p.main;
                        var d = p.main.startDelay;
                        d.constant = 1.5f;
                        m.startDelay = d;
                    }
                    p.Play();
                }
            }
        }

        private void StopParticleSystems()
        {
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                p.Stop();
            }
        }

        protected virtual void Awake()
        {          
            Starting = true;
            int fireLayer = UnityEngine.LayerMask.NameToLayer("FireLayer");
            UnityEngine.Physics.IgnoreLayerCollision(fireLayer, fireLayer);
            
        }

        protected virtual void Start()
        {
            mageStatus = GetComponentInParent<MageStatus>();
            firing = GetComponentInParent<Firing>();
        }

        public virtual void Fire()
        {
            if (AudioSource != null)
            {
                AudioSource.Play();
            }

            // if this effect has an explosion force, apply that now
            CreateExplosion(gameObject.transform.position, ForceRadius, ForceAmount);

            // start any particle system that is not in the list of manual start particle systems
            StartParticleSystems();

            // If we implement the ICollisionHandler interface, see if any of the children are forwarding
            // collision events. If they are, hook into them.
            ICollisionHandler handler = (this as ICollisionHandler);
            if (handler != null)
            {
                FireCollisionForwardScript collisionForwarder = GetComponentInChildren<FireCollisionForwardScript>();
                if (collisionForwarder != null)
                {
                    collisionForwarder.CollisionHandler = handler;
                }
            }
            
        }

        protected virtual void Update()
        {
            if (isStart)
            {
                timer += Time.deltaTime;
                mageStatus.ManaRegen(.2f);
                if (timer >= 10f)
                {
                    isStart = false;
                    Stop();
                    timer = 0;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q) && mageStatus.energy >= 100)
                {
                    isStart = true;
                    Fire();
                }
            }

        }

        public static void CreateExplosion(Vector3 pos, float radius, float force)
        {
            if (force <= 0.0f || radius <= 0.0f)
            {
                return;
            }

            // find all colliders and add explosive force
            Collider[] objects = UnityEngine.Physics.OverlapSphere(pos, radius);
            foreach (Collider h in objects)
            {
                Rigidbody r = h.GetComponent<Rigidbody>();
                if (r != null)
                {
                    r.AddExplosionForce(force, pos, radius);
                }
            }
        }

        public virtual void Stop()
        {

            StopParticleSystems();
            AudioSource.Stop();
        }

        public bool Starting
        {
            get;
            private set;
        }

        public float StartPercent
        {
            get;
            private set;
        }

        public bool Stopping
        {
            get;
            private set;
        }

        public float StopPercent
        {
            get;
            private set;
        }
    }
}
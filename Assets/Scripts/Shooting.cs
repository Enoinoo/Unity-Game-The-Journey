using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public int clipSize = 25;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource firingSound;
    public GameObject impactEffect;
    public Rigidbody rockets;
    public Transform shotPos;
    public GameObject rocketLauncher;
    public Rigidbody hugeRocket;
    public Transform rocketPos;
    public Image rightClick;
    public Image EKey;

    private int currentClip;
    private float timer;
    private float timeBetweenBullets = .15f;
    private Animator animator;
    private PlayerStatus playerStatus;

    private float rocketCD = 0f;
    private bool rocketInCD = false;
    private float reloadTimer = 0f;
    private bool healingCD = false;
    private float healingTimer = 0f;
    private bool isHealing = false;
    private float healAnimationTimer = 0f;
    private bool isRocketing = false;
    private float rocketCastTimer = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentClip = clipSize;
        animator = GetComponent<Animator>();
        playerStatus = GetComponentInParent<PlayerStatus>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (!animator.GetBool("isRunning"))
        {
            if (!isHealing && Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow)))
            {
                if(!isHealing && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow)))
                {
                    animator.SetBool("isRunning", true);

                }
                else
                {
                    animator.SetBool("isRunning", false);
                }
            }

            if (!isRocketing && !isHealing && !animator.GetBool("isReloading") && Input.GetKeyDown(KeyCode.Q) && playerStatus.energy >= 100)
            {
                animator.SetBool("isRunning", false);
                rocketLauncher.SetActive(true);
                animator.SetBool("isRocket", true);
                isRocketing = true;
                Invoke("LaunchRocket", 1f);
            }
            else if (isRocketing)
            {
                if (rocketCastTimer >= 2f)
                {
                    animator.SetBool("isRocket", false);
                    isRocketing = false;
                    playerStatus.energy = 0;
                    rocketCastTimer = 0f;
                    rocketLauncher.SetActive(false);
                }
                else rocketCastTimer += Time.deltaTime;
            }

            if (!isRocketing && !isHealing && Input.GetKeyDown(KeyCode.E) && !healingCD)
            {
                EKey.fillAmount = 1;
                animator.SetBool("isHealing", true);
                healingCD = true;
                isHealing = true;
            }
            if (!isReloading && !isRocketing && !isHealing && currentClip >= 0 && Input.GetMouseButton(0) && timer >= timeBetweenBullets)
            {
                animator.SetBool("isFiring", true);
                Shoot();
                if(currentClip > 0) currentClip -= 1;
                timer = 0;
                Invoke("StopShootAnim", .1f);
            }
            else if (!isRocketing && !isHealing && currentClip == 0)
            {
                Reload();
            }
            else if (!isRocketing && !isHealing && currentClip < clipSize && Input.GetKey(KeyCode.R))
            {
                Reload();
            }

            else if (!isRocketing && !isHealing && Input.GetMouseButtonDown(1) && (!rocketInCD || rocketCD >= 5f))
            {
                rightClick.fillAmount = 1;
                rocketInCD = true;
                animator.SetBool("isFiring", true);
                Rigidbody shot = Instantiate(rockets, shotPos.position, shotPos.rotation) as Rigidbody;
                shot.AddForce(shotPos.forward * 3500);
                Invoke("StopShootAnim", .1f);
            }

        }

        else
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", false);
            }
        }

        if (rocketInCD)
        {
            rightClick.fillAmount = 1 - rocketCD / 5;
            rocketCD += Time.deltaTime;
            if (rocketCD >= 5f)
            {
                rocketInCD = false;
                rocketCD = 0f;
            }
        }
        if (healingCD)
        {
            EKey.fillAmount = 1 - healingTimer / 6;
            healingTimer += Time.deltaTime;
            if (healingTimer >= 6f)
            {
                healingCD = false;
                healingTimer = 0f;
            }
        }
        if (isHealing)
        {
            healAnimationTimer += Time.deltaTime;

            if (healAnimationTimer >= .7f && healAnimationTimer <= 1.1f)
            {
                playerStatus.HealthRegen(3f);
            }
            if (healAnimationTimer >= .5f)
            {
                if (animator.GetBool("isHealing")) animator.SetBool("isHealing", false);
            }
            if (healAnimationTimer >= 1.2f)
            {
                isHealing = false;
                healAnimationTimer = 0f;
            }
        }
        
    }

    void StopShootAnim()
    {
        animator.SetBool("isFiring", false);

    }

    void Shoot()
    {
        muzzleFlash.Play();
        firingSound.Play();

        RaycastHit hit;
        LayerMask layerMask = 1 << 17;
        layerMask = ~layerMask;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            Target target = hit.transform.GetComponent<Target>();
            //Debug.Log(hit.transform.name);
            if (target != null)
            {
                if (!target.GetDying())
                {
                    target.TakeDamage(damage);
                    target.GainEnergy(damage / 2f);
                }
            }
        }

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        impactGO.GetComponent<ParticleSystem>().Play();
        Destroy(impactGO, 1f);
    }

    void Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        Invoke("FinishReload", 2);
    }
    void FinishReload()
    {
        currentClip = clipSize;
        animator.SetBool("isReloading", false);
        reloadTimer = 0f;
        isReloading = false;
    }

    void LaunchRocket()
    {
        Rigidbody shot = Instantiate(hugeRocket, rocketPos.position, rocketPos.rotation) as Rigidbody;
        shot.AddForce(rocketPos.forward * 3500);
    }

    public int GetCurrentClip()
    {
        return currentClip;
    }
}

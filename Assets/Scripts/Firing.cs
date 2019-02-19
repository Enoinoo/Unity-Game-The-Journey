using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {

    Animator animator;
    public GameObject flame;
    public GameObject lightning;
    public GameObject lightBeam;
    public GameObject lightBeamDamage;
    public ParticleSystem ActualLightBeam;

    private MageStatus mageStatus;
    private bool isCasting = false;
    private float timer = 0f;
    private float LBTimer = 0f;
    private float LBDamageStartTimer = 0f;
    private bool shouldWait = false;
    private bool LBActive = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        mageStatus = GetComponentInParent<MageStatus>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isCasting && !mageStatus.GetIsInvinc())
        {
            if (Input.GetKeyDown(KeyCode.Q) && mageStatus.energy >= 100)
            {
                isCasting = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isCasting", true);
                if (animator.GetBool("isFiring") || animator.GetBool("isLightning"))
                {
                    shouldWait = true;
                }
                if (shouldWait)
                {
                    var m = ActualLightBeam.main;
                    var d = ActualLightBeam.main.startDelay;
                    d.constant = .85f;
                    m.startDelay = d;
                }
                lightBeam.SetActive(true);
                LBActive = true;
                Stop();
            }
            else if (Input.GetMouseButton(0) && mageStatus.mana > 0)
            {
                if (!Input.GetMouseButton(1))
                {
                    animator.SetBool("isLightning", false);
                    animator.SetBool("isFiring", true);
                    animator.SetBool("isWalking", false);
                    lightning.SetActive(false);
                }
            }
            else if (Input.GetMouseButton(1))
            {
                if (!Input.GetMouseButton(0))
                {
                    animator.SetBool("isFiring", false);
                    animator.SetBool("isLightning", true);
                    animator.SetBool("isWalking", false);
                    lightning.SetActive(true);
                }
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                Stop();
                animator.SetBool("isWalking", true);
            }

            else
            {
                animator.SetBool("isWalking", false);
                Stop();
            }
        }
        else if (mageStatus.GetIsInvinc())
        {
            Stop();
        }
        else
        {
            timer += Time.deltaTime;
            if (LBActive)
            {
                LBDamageStartTimer += Time.deltaTime;
                if (!shouldWait && LBDamageStartTimer >= 1.2f) lightBeamDamage.SetActive(true);
                else if (shouldWait && LBDamageStartTimer >= 2f) lightBeamDamage.SetActive(true);
            }
            if (!shouldWait && LBTimer >= 1.1f) lightBeam.SetActive(false);
            else if (shouldWait && LBTimer >= 1.6f) lightBeam.SetActive(false);
            else LBTimer += Time.deltaTime;
            if (timer >= 10f)
            {
                isCasting = false;
                animator.SetBool("isCasting", false);
                timer = 0;
                LBTimer = 0;
                LBDamageStartTimer = 0;
                shouldWait = false;
                lightBeamDamage.SetActive(false);
                mageStatus.energy = 0f;
            }
        }
	}

    void Stop()
    {
        animator.SetBool("isFiring", false);
        animator.SetBool("isLightning", false);
        lightning.SetActive(false);
    }

    public bool GetIsCasting()
    {
        return isCasting;
    }
    
    public bool GetShouldWait()
    {
        return shouldWait;
    }
    
}

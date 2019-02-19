using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour {
    public int damage = 10;

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator OnTriggerEnter(Collider col)
    {
        if (col && animator.GetBool("isAttacking"))
        {
            yield return new WaitForSeconds(.75f);
            if (col)
            {
                if (col.GetComponentInChildren<MageStatus>())
                {
                    MageStatus mageStatus = FindObjectOfType<MageStatus>();
                    mageStatus.ApplyDamage(damage);
                }
                if (col.GetComponentInChildren<PlayerStatus>())
                {
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    playerStatus.ApplyDamage(damage);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingAI : MonoBehaviour
{
    public Transform target;
    public Animator anim;
    public float speed = .2f;
    public int longestDistance = 50;
    private Target t;
    private Vector3 deathPos;
    private Quaternion deathRot;

    void Start()
    {
        target = FindObjectOfType<MageStatus>() ? FindObjectOfType<MageStatus>().transform : FindObjectOfType<PlayerStatus>().transform;
        anim = GetComponent<Animator>();
        t = GetComponent<Target>();
    }

    void Update()
    {
        if (!t.GetDying())
        {
            Vector3 direction = target.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(target.position, this.transform.position) < longestDistance)
            {
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .1f);

                if (direction.magnitude > 10)
                {
                    if (!anim.GetBool("isAttacking"))
                    {
                        anim.SetBool("isWalking", true);
                        this.transform.Translate(0, 0, speed);
                    }
                    else return;
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", true);
                    Invoke("ResetAttack", 1.5f);
                }

            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            this.transform.position = deathPos;
            this.transform.rotation = deathRot;
        }
    }

    public void SetDeathPos(Vector3 dp)
    {
        deathPos = dp;
    }

    public void SetDeathRot(Quaternion dr)
    {
        deathRot = dr;
    }

    void ResetAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
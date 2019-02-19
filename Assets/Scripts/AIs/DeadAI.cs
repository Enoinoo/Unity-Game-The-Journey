using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAI : MonoBehaviour {

    private float startTime = 0f;
    private float generatedTime = 0f;
    private bool cast = false;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        generatedTime = Random.Range(0f, 2f);
        SetCast();
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime >= generatedTime)
        {
            if (cast)
            {
                Cast();
                Invoke("Attack", 1f);
            }
            else
            {
                Attack();
            }
        }
        else startTime += Time.deltaTime;

    }

    void SetCast()
    {
        if (Random.Range(0f, 1f) >= .3f)
        {
            cast = true;
        }
        else cast = false;

    }
    void Cast()
    {
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        gameObject.GetComponent<Animator>().SetBool("isCasting", true);
    }
    void Attack()
    {
        gameObject.GetComponent<Animator>().SetBool("isCasting", false);
        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAI : MonoBehaviour {

    private float startTime = 0f;
    private float generatedTime = 0f;
    private bool dying = false;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        generatedTime = Random.Range(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime >= generatedTime && !dying)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            dying = true;
        }
        else startTime += Time.deltaTime;
    }
}

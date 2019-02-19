using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour {

    private UnityStandardAssets.Characters.FirstPerson.MageController controller;
    //private Camera camera;
    private bool invinc = false;
    private int i = 0;

    // Use this for initialization
    void Start () {
        controller = GetComponent<UnityStandardAssets.Characters.FirstPerson.MageController>();
        //camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update () {
        if (invinc & i < 10)
        {
            gameObject.GetComponentInChildren<Camera>().fieldOfView -= 1;
            i++;
        }
        else if (!invinc & i > 0)
        {
            gameObject.GetComponentInChildren<Camera>().fieldOfView += 1;
            i--;
        }
	}

    public void GoInvincible()
    {
        controller.m_WalkSpeed = 50;
        gameObject.layer = 15;
        invinc = true;
        Invoke("SetCameraBack", .6f);
    }
    void SetCameraBack()
    {
        invinc = false;

    }
    public void ExitInvincible()
    {
        controller.m_WalkSpeed = 15;
        gameObject.layer = 12;
    }
}

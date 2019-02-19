using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {

    private Shooting shooting;
    private Text text;
    private string clip;
    private int clipSize;


    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        shooting = FindObjectOfType<Shooting>();
        clipSize = shooting.clipSize;
        clip = " / " + clipSize.ToString();
    }

    // Update is called once per frame
    void Update () {
        int currentClip = shooting.GetCurrentClip();

        text.text = currentClip.ToString() + clip;
	}
}

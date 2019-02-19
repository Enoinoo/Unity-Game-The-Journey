using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Timer : MonoBehaviour {

    public float timer = 0;
    public Image left;
    public Image right;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        left.fillAmount = (100 - timer) / 100f;
        right.fillAmount = (100 - timer) / 100f;
        if (left.fillAmount <= 0)
        {
            SceneManager.LoadScene("Win");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}
}

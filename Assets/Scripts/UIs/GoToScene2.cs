using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene2 : MonoBehaviour {

    public CharacterSelect characterSelect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (characterSelect.isSoldier)
            SceneManager.LoadScene("Level2");
        else SceneManager.LoadScene("Level2Mage");
    }
}

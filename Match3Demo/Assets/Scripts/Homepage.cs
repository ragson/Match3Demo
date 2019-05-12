using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homepage : MonoBehaviour {


    public GameObject m_gamepage;
    public GameObject m_homepage;
    // Use this for initialization
    void Start() {

    }

    public void OpenGame()
    {
        m_homepage.SetActive(false);
        m_gamepage.SetActive(true);
    }


	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


	}

}

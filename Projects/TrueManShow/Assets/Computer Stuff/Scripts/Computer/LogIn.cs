using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LogIn : MonoBehaviour {

    public string desiredPass;
    public GameObject password;
    public GameObject desiredWindow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckPass()
    {
        if (password.GetComponent<Text>().text == desiredPass)
        {
            this.transform.parent.gameObject.SetActive(false);
            desiredWindow.SetActive(true);

        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMail : MonoBehaviour {

    public GameObject mail;
    public GameObject mailWindow;
    public GameObject resetButtons;

    public void ResetMail()
    {

        mail.SetActive(false);
        mailWindow.SetActive(false);
        resetButtons.GetComponent<ResetMailButtons>().ResetAllButtons();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

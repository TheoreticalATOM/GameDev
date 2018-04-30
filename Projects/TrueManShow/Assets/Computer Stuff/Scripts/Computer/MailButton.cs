using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailButton : MonoBehaviour {
    public Mail mail;
    public GameObject mailBox;

	// Use this for initialization
	void Start () {
        this.transform.Find("FromText").GetComponent<Text>().text = mail.sender;
        this.transform.Find("SubjectText").GetComponent<Text>().text = mail.subject;

    }

    public void ShowMail()
    {
        if(mailBox.activeSelf == false)
        {
            mailBox.SetActive(true);
        }
        mailBox.transform.Find("FromText").GetComponent<Text>().text = mail.sender;
        mailBox.transform.Find("ToText").GetComponent<Text>().text = mail.receiver;
        mailBox.transform.Find("SubjectText").GetComponent<Text>().text = mail.subject;
        mailBox.transform.Find("TextBody").GetComponent<Text>().text = mail.text;
        mailBox.transform.Find("DateText").GetComponent<Text>().text = mail.date;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

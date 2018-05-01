using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	public GameObject controllPanel;
	public GameObject optionsPanel;
	public GameObject creditsPanel;

	public GameObject CreditsPanel2;
	public GameObject otherPanel;

	void Start () 
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}

	public void CloseAllPanels()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}

	public void controls()
	{
		controllPanel.SetActive(true);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}

		public void Options()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(true);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}

		public void credits()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(true);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}

		public void other()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(true);
		CreditsPanel2.SetActive(false);
	}

		public void nextCredit()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(true);
	}
		public void prevCredit()
	{
		controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(true);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
	}
	    public void Quit()
    {
        Application.Quit();
    }

	public void playGame()
	{
		SceneManager.LoadScene(1);
	}


}

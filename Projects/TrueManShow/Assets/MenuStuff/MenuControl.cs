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

    public GameObject LoadyBoi;
    public GameObject PlayBut;

    private int index;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;
    int activeScreenResindex;

    void Start () 
	{
        activeScreenResindex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResindex;
        }

        controllPanel.SetActive(false);
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		otherPanel.SetActive(false);
		CreditsPanel2.SetActive(false);
        LoadyBoi.SetActive(false);
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
        LoadyBoi.SetActive(true);
        PlayBut.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(LoadLevelDelayRoutine());
    }

    IEnumerator LoadLevelDelayRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadSceneAsync(1);
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResindex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResindex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullScreen;
        }
        if (isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResindex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();

    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

}

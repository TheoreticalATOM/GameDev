using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

 	public GameObject PauseUI;
 	private bool paused = false;
    public Player player;

	void Start () 
	{
		PauseUI.SetActive(false);
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Pause"))
        {
            paused = !paused;

            if (paused)
            {
                PauseUI.SetActive(true);
                Time.timeScale = 1;
                player.Lock(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            if (!paused)
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1;
                player.Lock(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
	}

	public void Resume()
    {
        paused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

	public void Quit()
    {
        Application.Quit();
    }
}

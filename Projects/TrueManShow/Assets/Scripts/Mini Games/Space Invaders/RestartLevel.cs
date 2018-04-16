using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerScore.playerScore = 0;
            SIGameOver.isPlayerDead = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("Scene_01");
        }
		
	}
}

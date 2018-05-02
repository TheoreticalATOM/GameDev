using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class BasketScore : MonoBehaviour {

	public int currentScore;
	private Text textScore;
	private AudioSource audioSource;
	public AudioClip scoreSound;

	private void Awake()
	{
		if (!audioSource)
			audioSource = GetComponent<AudioSource>();
	}


	// Use this for initialization
	void Start () {
		currentScore = 0;
		textScore = GetComponent<Text>();
		textScore.text = "" + currentScore;
		audioSource.clip = scoreSound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore ()
	{
		currentScore++;
		textScore.text = "" + currentScore;
		audioSource.clip = scoreSound;
		audioSource.Play();
	}
}

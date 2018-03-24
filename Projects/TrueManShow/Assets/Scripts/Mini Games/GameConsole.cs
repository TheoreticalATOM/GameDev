using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConsole : MonoBehaviour
{
	public MiniGame CurrentGame;

	public void StartGame()
	{
		CurrentGame.Play();
	}

	public void EndGame()
	{
		CurrentGame.End();
	}
}

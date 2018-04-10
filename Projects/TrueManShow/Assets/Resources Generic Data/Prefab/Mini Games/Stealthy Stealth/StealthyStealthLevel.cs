using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthyStealthLevel : MonoBehaviour
{
    public Transform SpawnPoint;

    public void Play(StealthyStealthPlayer player)
    {
        player.SetSpawnPoint(SpawnPoint.position);
        gameObject.SetActive(true);
		player.Respawn();
    }

	public void End()
	{
		gameObject.SetActive(false);
	}
}

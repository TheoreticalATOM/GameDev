using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.Data;
using SDE.GamePool;

public class ToaderSpawner : MonoBehaviour
{
    public RuntimeSet Pool;
	public GameObject Car;
	public float ZRotation;
    public float MinDelay;
    public float MaxDelay;

    private void OnEnable()
    {
		StartCoroutine(SpawningRoutine());
    }

	private IEnumerator SpawningRoutine()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(MinDelay, MaxDelay));
			Pool.GetFirst<GamePool>().Spawn(Car, transform.position, Quaternion.Euler(0.0f, 0.0f, ZRotation));
		}
	}

}

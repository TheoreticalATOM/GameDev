using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using UnityEngine;

public class StealthyStealthLevelProgressor : MonoBehaviour
{
    [SerializeField] private AudioClip Clip;
    public RuntimeSet AudioPoolSet;
    public StealthyStealth Game;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioPoolSet.GetFirst<AudioPool>().PlayClip(Clip);
		Game.NextLevel();
    }
}

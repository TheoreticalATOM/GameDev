using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylPlayer : MonoBehaviour {

    private Vinyl mcurrentMusic;

    public void setCurrentMusic(Vinyl currentMusic)
    {
        mcurrentMusic = currentMusic;
    }

    public void StartMusic()
    {
        if (mcurrentMusic)
        {
           mcurrentMusic.OnPlay();
        }
    }

    public void PauseMusic()
    {
        mcurrentMusic.OnPaused();
    }

    public void StopMusic()
    {
        mcurrentMusic.OnEnded();
    }


    private void Start()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylPlayer : MonoBehaviour
{
    public Animator PinAnimator;
    public Rotator HandleRotator;
    private Vinyl mcurrentMusic;

    public void setCurrentMusic(Vinyl currentMusic)
    {
        mcurrentMusic = currentMusic;
    }

    public void StartMusic()
    {
        if (mcurrentMusic)
        {
            PinAnimator.SetBool("play", true);
            HandleRotator.SetRotate(true);
            mcurrentMusic.OnPlay();
        }
    }

    public void PauseMusic()
    {
        PinAnimator.SetBool("play", false);
        HandleRotator.SetRotate(false);
        mcurrentMusic.OnPaused();
    }

    public void StopMusic()
    {
        PinAnimator.SetBool("play", false);
        HandleRotator.SetRotate(false);
        mcurrentMusic.OnEnded();
    }
}
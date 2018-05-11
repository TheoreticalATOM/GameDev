using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterAnim : MonoBehaviour {

    public AudioClip finalTwist;

    public void printEvent()
    {
        print("yoo");
        this.GetComponent<AudioSource>().clip = finalTwist;
        this.GetComponent<AudioSource>().enabled = true;
        this.GetComponent<AudioSource>().Play();
    }

    public void Playsong()
    {
        print("play");
        this.GetComponent<AudioSource>().enabled = true;
        this.GetComponent<AudioSource>().Play();

        StartCoroutine(OnDelayed());
    }

    public void Stopsong()
    {
        this.GetComponent<AudioSource>().Stop();
    }

    private IEnumerator OnDelayed()
    {
        yield return new WaitForSeconds(finalTwist.length + 2.0f);
        SceneManager.LoadScene(0);
    }

}

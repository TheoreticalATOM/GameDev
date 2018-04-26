using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(AudioSource))]
public class GameOver : MonoBehaviour
{
	public float MinWaitDuration;
    public DialogPlayer DialogOnCaught;
    
    private Image mImage;
    private AudioSource mSource;
    
    private void Start()
    {
        mSource = GetComponent<AudioSource>();
        mImage = GetComponent<Image>();
        mImage.enabled = false;
    }

    public void ShowGameOver(string sceneName, System.Action gameoverCallback)
    {
        mImage.enabled = true;
        DialogOnCaught.OnCompleted.AddListener(() =>
        {
            StartCoroutine(GameOverRoutine(sceneName, gameoverCallback));
        });
        DialogOnCaught.Play();
    }

    IEnumerator GameOverRoutine(string sceneName, System.Action gameoverCallback)
    {
        mSource.Play();
        yield return new WaitForSeconds(MinWaitDuration);
        gameoverCallback();

		var scene = SceneManager.LoadSceneAsync(sceneName);
		while(scene.progress < 1.0f)
			yield return new WaitForEndOfFrame();
    }

    private void OnDestroy()
    {
        DialogOnCaught.OnCompleted.RemoveAllListeners();
    }
}

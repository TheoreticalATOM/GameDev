using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public float MinWaitDuration;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameOver(string sceneName, System.Action gameoverCallback)
    {
        gameObject.SetActive(true);
        StartCoroutine(GameOverRoutine(sceneName, gameoverCallback));
    }

    IEnumerator GameOverRoutine(string sceneName, System.Action gameoverCallback)
    {
        yield return new WaitForSeconds(MinWaitDuration);
        gameoverCallback();

		var scene = SceneManager.LoadSceneAsync(sceneName);
		while(scene.progress < 1.0f)
			yield return new WaitForEndOfFrame();
    }
}

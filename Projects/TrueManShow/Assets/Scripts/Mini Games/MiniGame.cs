using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
	[System.Serializable]
	public class GameView
	{
		public Camera camera;
		public float Size;
		public Color BackgroundColor;
	}

    public Material ExternalView;
    public UnityEvent OnGameCompleted;

    protected bool IsPaused { get; private set; }

    // _________________________________________________________
    // @ Controls
    #region Controls
    public void CompleteGame()
    {
        OnGameCompleted.Invoke();
    }

    public void Play()
    {
        gameObject.SetActive(true);
        IsPaused = false;
        OnPlay();
    }

    public void Pause(bool pauseState)
    {
        IsPaused = pauseState;
        OnPaused();
    }

    public void End()
    {
        gameObject.SetActive(false);
        OnEnded();
    }
    #endregion
    // _________________________________________________________
    // @ Inheritance
    protected abstract void OnInit();
    protected abstract void OnPlay();
    protected virtual void OnPaused() { }
    protected virtual void OnEnded() { }
    protected virtual void OnUpdate() { enabled = false;}

    // _________________________________________________________
    // @ Methods
    private void Start()
    {
        //gameObject.SetActive(false);
        OnInit();
    }
    private void Update()
    {
        if (!IsPaused)
            OnUpdate();
    }
}

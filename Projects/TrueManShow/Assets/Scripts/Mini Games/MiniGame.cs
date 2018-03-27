using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public abstract class MiniGame : SerializedMonoBehaviour
{
    [System.Serializable]
    public class GameView
    {
        public float Size;
        public Color BackgroundColor;
        public Transform Target;
        public bool Follow = false;
    }

    public MiniGameCamera Camera;
    public GameView View;
    public UnityEvent OnGameCompleted;

    protected bool IsPaused { get; private set; }

    // _________________________________________________________
    // @ Controls
    #region Controls
    public void CompleteGame()
    {
        OnGameCompleted.Invoke();
    }

    [Button]
    public void Play()
    {
        gameObject.SetActive(true);
        IsPaused = false;
        Camera.SetTarget(View);

        OnPlay();
    }

    public void Pause(bool pauseState)
    {
        IsPaused = pauseState;
        OnPaused();
    }

    [Button]
    public void End()
    {
        gameObject.SetActive(false);
        OnEnded();
        Camera.ClearTarget();
    }
    #endregion
    // _________________________________________________________
    // @ Inheritance
    protected abstract void OnInit();
    protected abstract void OnPlay();
    protected virtual void OnPaused() { }
    protected virtual void OnEnded() { }
    protected virtual void OnUpdate() { enabled = false; }

    // _________________________________________________________
    // @ Methods
    private void Start()
    {
        gameObject.SetActive(false);
        OnInit();
    }
    private void Update()
    {
        if (!IsPaused)
            OnUpdate();
    }
}

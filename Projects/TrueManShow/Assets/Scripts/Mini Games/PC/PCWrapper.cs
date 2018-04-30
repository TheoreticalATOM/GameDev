using UnityEngine;
using UnityEngine.UI;

public class PCWrapper : MonoBehaviour
{
    public GameObject Computer;
    public GameObject Desktop;
    public GameObject Login;
    
    private bool mIsRunning;

    private void Start()
    {
        TurnOff();
        enabled = false;
    }
    
    
    public void TurnOn()
    {
        Desktop.SetActive(false);
        Login.SetActive(true);
        
        mIsRunning = true;
        Computer.SetActive(true);
    }
    
    public void TurnOff()
    {
        mIsRunning = false;
        Computer.SetActive(false);
    }

    public void ShowCursor(bool state)
    {
        if (!mIsRunning)
            return;

        Cursor.visible = state;
        Cursor.lockState = (state) ? CursorLockMode.None : CursorLockMode.Locked;
    }
}


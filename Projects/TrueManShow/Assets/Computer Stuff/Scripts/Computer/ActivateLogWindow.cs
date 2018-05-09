using UnityEngine;

public class ActivateLogWindow : MonoBehaviour {

    public GameObject mailWindow;
    public void active()
    {
        if (!mailWindow.activeSelf)
            this.gameObject.SetActive(true);
    }
}

using UnityEngine.UI;
using UnityEngine;

using SDE.UI;

public class AutoQueue : MonoBehaviour
{
	public GameObject TaskParent;
    public Text Title;
    public Text Body;
    public Text CustomMessage;
    public Progress TimeBar;
    public GameObject LiveIndicator;
    public AudioSource CustomMessageSound;
    
    public void SetCustomMessage(string msg)
    {
        CustomMessage.gameObject.SetActive(true);
        CustomMessageSound.Play();
        CustomMessage.text = msg;
        Title.gameObject.SetActive(false);
        Body.gameObject.SetActive(false);
    }
    public void ClearCustomMessage()
    {
        CustomMessage.gameObject.SetActive(false);
        Title.gameObject.SetActive(true);
        Body.gameObject.SetActive(true);
    }
    
    public void SetToTaskDescription(Task tasks)
    {
        ClearCustomMessage();
        SetQueue(tasks.TaskName, tasks.Description);
    }

    public void SetQueue(string title, string body)
    {
        TaskParent.SetActive(true);
        Title.text = title;
        Body.text = body;
    }
}

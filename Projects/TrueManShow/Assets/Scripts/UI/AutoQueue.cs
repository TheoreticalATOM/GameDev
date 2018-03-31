using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using SDE.UI;

public class AutoQueue : MonoBehaviour
{
	public GameObject TaskParent;
    public Text Title;
    public Text Body;
    public Progress TimeBar;
    public GameObject LiveIndicator;


    public void SetToTaskDescription(Task tasks)
    {
		SetQueue(tasks.TaskName, tasks.Description);
    }

    public void SetQueue(string title, string body)
    {
        TaskParent.SetActive(true);
        Title.text = title;
        Body.text = body;
    }
}

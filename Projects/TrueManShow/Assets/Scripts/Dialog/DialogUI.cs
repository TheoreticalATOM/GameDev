using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : SerializedMonoBehaviour, IRuntime
{
    public RuntimeSet DialogUISet;
    public Text TextArea;
	public Text SecondaryTextArea;
	public int StackLimit = 1;
	
	public GameObject BackDrop;
	public float BackDropHideDelay = 0.2f;
	
    private Queue<string> mQueuedText;
	
    private void Awake()
    {
		mQueuedText = new Queue<string>();
		TextArea.text = string.Empty;
	    SecondaryTextArea.text = string.Empty;
        DialogUISet.Add(this);
    }

	private void OnDestroy()
	{
		DialogUISet.Remove(this);
	}

	public void SetSecondaryText(string text)
	{
		if (text.Length == 0)
			return;
		
		SecondaryTextArea.text = text;
		SetBackdrop(true);
	}
	public void ClearSecondaryText()
	{
		SecondaryTextArea.text = string.Empty;
		SetBackdrop(false);
	}
	
	
	public void SetText(string text)
    {
	    if (text.Length == 0)
		    return;
	    
	    SetBackdrop(true);
		mQueuedText.Enqueue(text);
		if(mQueuedText.Count > StackLimit)
			mQueuedText.Dequeue();

		TextArea.text = string.Empty;
		foreach (var textElement in mQueuedText)
			TextArea.text += textElement + "\n";
	    
	    StopAllCoroutines();
    }

	public void ClearText()
	{
		StartCoroutine(ClearingBackDropRoutine());
		mQueuedText.Clear();
		TextArea.text = string.Empty;
	}

	private void SetBackdrop(bool value)
	{
		if (BackDrop)
		{
			if(value) BackDrop.SetActive(true);
			else if(TextArea.text == string.Empty && SecondaryTextArea.text == string.Empty)
				BackDrop.SetActive(false);
		}
	}

	IEnumerator ClearingBackDropRoutine()
	{
		yield return new WaitForSeconds(BackDropHideDelay);
		//SetBackdrop(false);
	}
	
}

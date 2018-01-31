using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour, IRuntime
{
	public RuntimeSet DialogUISet;
	public Text TextArea;

	private void Awake() {
		DialogUISet.Add(this);
	}

	public void SetText(string text)
	{
		TextArea.text = text;
	}
}

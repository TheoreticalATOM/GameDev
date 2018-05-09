using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour {

    [TextArea (1,5)]
    public string desiredText;
    private string currtext;
    public GameObject NextText;

	// Use this for initialization
	void Start () {
        currtext = GetComponent<Text>().text;
	}

    public void StartTextAppear()
    {
        StartCoroutine(AnimateText(desiredText));

    }

    public void StopTextAppear()
    {
        if(NextText != null)
            NextText.GetComponent<TextAppear>().StartTextAppear();
    }

    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        currtext = "";
        while (i < strComplete.Length)
        {
            currtext += strComplete[i++];
            GetComponent<Text>().text = currtext;
            yield return new WaitForSeconds(0.05F);
        }
        StopTextAppear();
    }
    
    public void ResetText()
    {
        GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update () {

    }
}

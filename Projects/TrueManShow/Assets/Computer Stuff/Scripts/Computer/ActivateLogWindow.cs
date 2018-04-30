using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLogWindow : MonoBehaviour {

    public GameObject mailWindow;

    public void active()
    {
        if (!mailWindow.activeSelf)
            this.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

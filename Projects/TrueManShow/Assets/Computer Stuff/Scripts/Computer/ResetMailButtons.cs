using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetMailButtons : MonoBehaviour {
    private GameObject previousButton;
    public Sprite originalSprite;

    public void ResetButtons(GameObject buttonPressed)
    {
        if (previousButton == null)
        {
            previousButton = buttonPressed;
        }
        else
        {
            if(previousButton != buttonPressed)
            {
                previousButton.GetComponent<Image>().sprite = originalSprite;
                previousButton = buttonPressed;
            }

        }
    }
    public void ResetAllButtons()
    {
        if(previousButton != null)
        {
            previousButton.GetComponent<Image>().sprite = originalSprite;
            previousButton = null;
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour {
    public Sprite imageOnPressed;
    public GameObject powerButton;
    private Sprite originalImage;
    public void ChangeSprite()
    {
        if (GetComponent<Image>().sprite == originalImage)
        {
            GetComponent<Image>().sprite = imageOnPressed;
        }
        else
        {
            GetComponent<Image>().sprite = originalImage;
            print(GetComponent<Image>().sprite);
        }
    }

    public void ChangeMailBoxSprite()
    {
        GetComponent<Image>().sprite = imageOnPressed;
    }

    public void TogglePower()
    {
        if (powerButton.activeSelf)
        {
            powerButton.SetActive(false);
        }
        else
        {
            powerButton.SetActive(true);
        }
    }

	// Use this for initialization
	void Start () {
        originalImage = GetComponent<Image>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

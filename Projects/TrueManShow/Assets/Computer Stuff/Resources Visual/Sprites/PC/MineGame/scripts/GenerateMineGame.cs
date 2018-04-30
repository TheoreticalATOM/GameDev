using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMineGame : MonoBehaviour {
    public int width = 10;
    public int height = 13;
    public GameObject MineButton;
    public GameObject smille;
    public Sprite normSmille;
    public Sprite winSprite;
    public Sprite lossSprite;

    public bool gameOver = false;


    private float x;
    private float y;
    private GameObject currButton;
    public void GenerateGame()
    {
        x = 0;//this.transform.localPosition.x;
        y = 0;//this.transform.localPosition.y;
        for(int i = 1; i <= width; i++)
        {
            for( int j = 1; j <= height; j++)
            {
                currButton = Instantiate(MineButton);
                currButton.transform.SetParent(transform);

                RectTransform rect = currButton.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(x,y,0.0f);
                rect.localRotation = Quaternion.identity;
                rect.localScale = Vector3.one;
                
                currButton.GetComponent<Element>().x = i - 1;
                currButton.GetComponent<Element>().y = j - 1;
                
                
                //currButton = Instantiate(MineButton, Vector3.zero,this.transform.rotation, transform);
                
                Grid.elements[i-1, j-1] = currButton.GetComponent<Element>();
                x = x + 15;
            }
            x = 0.0f;
            y = y - 15;
        }
    }

    public void DestroyGame()
    {
        smille.GetComponent<Image>().sprite = normSmille;
        gameOver = false;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).name == "Button(Clone)")
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
                
        }
    }

    public void RestarGame()
    {
        gameOver = false;
        DestroyGame();
        GenerateGame();
    }

    public void WonGame()
    {
        gameOver = true;
        smille.GetComponent<Image>().sprite = lossSprite;
    }

    public void LossGame()
    {
        gameOver = true;
        smille.GetComponent<Image>().sprite = winSprite;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

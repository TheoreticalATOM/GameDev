using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour {


    public bool mine;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public int x;
    public int y;


    // Use this for initialization
    void Start () {

        mine = Random.value < 0.15;

    }

    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
        {
            GetComponent<Image>().sprite = mineTexture;
        }
        else
        {
            GetComponent<Image>().sprite = emptyTextures[adjacentCount];
        }
            
    }

    public bool isCovered()
    {
        return GetComponent<Image>().sprite.texture.name == "MineButton";
    }

    public void OnClicked()
    {
        if (!this.transform.parent.GetComponent<GenerateMineGame>().gameOver)
        {
            if (mine)
            {
                Grid.uncoverMines();
                this.transform.parent.GetComponent<GenerateMineGame>().WonGame();
            }
            else
            {
                loadTexture(Grid.adjacentMines(x, y));

                // uncover area without mines
                Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

                // find out if the game was won now
                if (Grid.isFinished())
                {
                    this.transform.parent.GetComponent<GenerateMineGame>().LossGame();
                }
                    
            }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}

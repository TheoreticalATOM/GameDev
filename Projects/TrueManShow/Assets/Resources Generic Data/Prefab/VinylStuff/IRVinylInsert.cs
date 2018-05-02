using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRVinylInsert : InventoryResponse
{

    public VinylPlayer vinylPlayer;
    public Vinyl Music;

    private void Reset()
    {
       // Console = FindObjectOfType<GameConsole>();
    }

    protected override void OnFailResponse()
    {

    }
    protected override void OnSuccessResponse()
    {
       vinylPlayer.setCurrentMusic(Music);
    }
}

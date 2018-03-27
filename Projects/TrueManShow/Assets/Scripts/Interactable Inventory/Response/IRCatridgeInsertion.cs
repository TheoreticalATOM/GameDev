using UnityEngine;

public class IRCatridgeInsertion : InventoryResponse
{
    public GameConsole Console;
    public MiniGame MiniGame;

    private void Reset() 
    {
        Console = FindObjectOfType<GameConsole>();
    }

    protected override void OnFailResponse()
    {

    }
    protected override void OnSuccessResponse()
    {
        Console.SetCurrentGame(MiniGame);
    }
}
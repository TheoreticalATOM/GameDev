using UnityEngine;
using UnityEngine.Events;

public class IRCatridgeInsertion : InventoryResponse
{
    public GameConsole Console;
    public MiniGame MiniGame;
    public UnityEvent GameInserted;
    
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
        GameInserted.Invoke();
    }
}
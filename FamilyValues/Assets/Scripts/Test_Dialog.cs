using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test_Dialog : SerializedMonoBehaviour
{
    public DialogTree Tree;

    public ScriptableEnum Down;
    public ScriptableEnum Right;
	
	public DialogNode DownDialog;
	public ScriptableEnum PositiveDirection;
	public ScriptableEnum NegativeDirection;

    [Button]
    public void GoDown()
    {
        Tree.ActiveNode.SetDirection(Down);
    }
    [Button]
    public void GoRight()
    {
        Tree.ActiveNode.SetDirection(Right);
    }

	[Button]
	public void DownPositive()
	{
		DownDialog.SetDirection(PositiveDirection);
	}

	[Button]
	public void DownNegative()
	{
		DownDialog.SetDirection(NegativeDirection);
	}

    [Button]
    public void Play()
    {
        Tree.Play();
    }
}

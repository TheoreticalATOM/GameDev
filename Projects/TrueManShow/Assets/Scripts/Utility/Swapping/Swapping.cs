using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Swapping<T> : MonoBehaviour 
{
    public T SwapTo;
    private T mSwapFrom;
	private bool mToggle = true;
    private void Start()
    {
		SetDefault(out mSwapFrom);
    }

	public void Swap()
	{
		T swap = (mToggle) ? SwapTo : mSwapFrom;
		mToggle = !mToggle;
		
		OnSwapped(swap);
	}

    public abstract void SetDefault(out T swapFrom);
	protected abstract void OnSwapped(T swappedTo);
}

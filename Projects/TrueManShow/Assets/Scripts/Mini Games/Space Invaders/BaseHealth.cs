using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDE;


public class BaseHealth : MonoBehaviour {

    public float health = 2;
	public Sprite sprite2;
	private SpriteRenderer SpriteRenderer;

	private float mCurrentHealth;
	private Sprite mOrigSprite;

	public event System.Action OnNoHealth; 
	
	// Use this for initialization
	void Awake () {
		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		mCurrentHealth = health;
		mOrigSprite = SpriteRenderer.sprite;
	}

	public void ResetHealth()
	{
		gameObject.SetActive(true);
		mCurrentHealth = health;
		SpriteRenderer.sprite = mOrigSprite;
	}

	public void Damage()
	{
		if (--mCurrentHealth == 1)
			SpriteRenderer.sprite = sprite2;
		if (mCurrentHealth <= 0)
		{
			gameObject.SetActive(false);
			OnNoHealth.TryInvoke();
		}
	}
	
//	// Update is called once per frame
//	void Update () {
//		if (mCurrentHealth == 1)
//			SpriteRenderer.sprite = sprite2;
//        if (mCurrentHealth <= 0)
//            gameObject.SetActive(false);
//	}
}

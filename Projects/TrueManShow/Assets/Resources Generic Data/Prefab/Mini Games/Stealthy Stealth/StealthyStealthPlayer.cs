using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.UI;
using SDE;
using SDE.Data;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class StealthyStealthPlayer : MonoBehaviour
{
		// ________________________________________________
	// @ Events
    public event System.Action OnDied;

	// ________________________________________________
	// @ Inspector
    public float Speed;
    public float MaxHealth = 100.0f;
    public Progress SuspicionMeter;
	public RuntimeSet AudioPoolSet;
	public AudioClip WalkingClip;
	
	// ________________________________________________
	// @ Data
    private Rigidbody2D mBody;
    private SpriteRenderer mSprite;
    private Animator mAnimator;
	private Vector3 mSpawnPoint;
	
	private AudioPool mPool;
	private AudioSource mLoopedSource;
	
	
	// ________________________________________________
	// @ Getters
    public float CurrentHealth { get; private set; }

	// ________________________________________________
	// @ Controls
    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        SuspicionMeter.UpdateProgress(CurrentHealth, MaxHealth);
    }

    public void Damage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - Mathf.Abs(amount), 0.0f);
        SuspicionMeter.UpdateProgress(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
            OnDied.TryInvoke();
    }

	public void SetSpawnPoint(Vector3 spawnPoint)
	{
		mSpawnPoint = spawnPoint;
	}

	public void Respawn()
	{
		transform.position = mSpawnPoint;
	}

	public void LockPlayer(bool state)
	{
		enabled = !state;
        if(state)
        {
		    mBody.velocity = Vector2.zero;
            mAnimator.SetFloat("walk", 0.0f);
        }
	}

	// ________________________________________________
	// @ Methods
    private void Awake()
    {
        mBody = GetComponent<Rigidbody2D>();
        mSprite = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
    }

	private void Start()
	{
		mPool = AudioPoolSet.GetFirst<AudioPool>();
	}

	private void OnEnable()
    {
        ResetHealth();
    }

    private void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        // flip player
        if (hInput < 0.0f) mSprite.flipX = true;
        else if (hInput > 0.0f) mSprite.flipX = false;

        // Animate Player
        mAnimator.SetFloat("walk", Mathf.Abs(hInput) + Mathf.Abs(vInput));

        // Move Player
        mBody.velocity = Speed * new Vector2(hInput, vInput);
    }
}

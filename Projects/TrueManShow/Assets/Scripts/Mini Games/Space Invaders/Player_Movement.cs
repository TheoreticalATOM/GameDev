using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	private Transform player;
	public float speed;
	public float maxBound, MinBound;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;
	private Vector3 mStartPos;
	
	// Use this for initialization
	void Awake () {
		player = GetComponent<Transform>();
		mStartPos = player.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		if (player.localPosition.x < MinBound && h < 0)
			h = 0;
		else if (player.localPosition.x > maxBound && h > 0)
			h = 0;
		player.localPosition += Vector3.right * h * speed;
		
	}

	public void ResetPlayer()
	{
		gameObject.SetActive(true);
		player.position = mStartPos;
	}
	

	void Update()
	{
        if (Input.GetButton("MiniGameStart") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
	}
}

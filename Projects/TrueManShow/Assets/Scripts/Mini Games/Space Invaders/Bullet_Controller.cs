using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour {

	private Transform Bullet;
	public float speed;

	private float mStartPosY;
	
	// Use this for initialization
	void Start () {
		Bullet = GetComponent<Transform> ();
		mStartPosY = Bullet.position.y;
	}

	void FixedUpdate(){
		Bullet.position += Vector3.up * speed;
		float displacement = Bullet.position.y - mStartPosY;
		
		if(displacement >= 20.0f)
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D Other){
		if (Other.tag == "Enemy") 
		{
			Other.gameObject.SetActive(false);
			//Destroy (Other.gameObject);
			Destroy (gameObject);
			EnemyController.ReduceEnemyCount();
			MiniGameSpaceInvaders.AddScore(10);
		} else if (Other.tag == "Base") {
			Destroy (gameObject);
		
		}
		
	}
}

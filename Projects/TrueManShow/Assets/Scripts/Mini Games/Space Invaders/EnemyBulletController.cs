using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {

    private Transform bullet;
    public float speed;

	private float mStartPosY;

	
	// Use this for initialization
	void Start () {

        bullet = GetComponent<Transform>();
		bullet.position = new Vector3(bullet.position.x,bullet.position.y,0);
		
		mStartPosY = bullet.position.y;
	}

    void FixedUpdate()
    {
        bullet.position += Vector3.up * -speed;
		//bullet.position = new Vector3(bullet.position.x,bullet.position.y,0);
	    
	    float displacement = bullet.position.y - mStartPosY;

        if (displacement < -20)
            Destroy(bullet.gameObject);
    }

	void OnTriggerEnter2D(Collider2D Other){
		print ("Coll");
        if (Other.tag == "Player") {
			print ("PLayer collision");
	        Other.gameObject.SetActive(false);
	        MiniGameSpaceInvaders.EndCurrentGame();
            Destroy(gameObject);
        } else if (Other.tag == "Base")
        {
            GameObject playerBase = Other.gameObject;
            BaseHealth baseHealth = playerBase.GetComponent<BaseHealth>();
            baseHealth.Damage();
            Destroy(gameObject);
        }
    
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBasketScore : MonoBehaviour {
	
	public GameObject Scoretext;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Basket Ball") 
		{
			Scoretext.GetComponent<BasketScore>().AddScore();
			//script.AddScore ();
		}
	}


}

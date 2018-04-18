using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleChanger : MonoBehaviour
{
	public float ScaleChangeSpeed = 10.0f;
	
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			Time.timeScale -= ScaleChangeSpeed * Time.deltaTime;
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			Time.timeScale += ScaleChangeSpeed * Time.deltaTime;

		if (Input.GetKeyUp(KeyCode.Space))
			Time.timeScale = 1.0f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatromino : MonoBehaviour
{
	const float TILE_SIZE = 8.0f;
	public Vector2 Offset;

	public void MoveDown()
	{
		transform.Translate(0.0f, -TILE_SIZE, 0.0f, Space.World);
	}

	public void MoveHorizontal(float dir)
	{
		transform.Translate(TILE_SIZE * dir, 0.0f, 0.0f, Space.World);
	}

	public void Rotate()
	{
		transform.Rotate(Vector3.forward, 90.0f, Space.World);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.Input;

public class ToaderPlayer : MonoBehaviour
{
    private AxisUp mVInput = new AxisUp();
    private AxisUp mHInput = new AxisUp();

    private readonly Quaternion UP_ROT = Quaternion.Euler(0.0f, 0.0f, -90.0f);
    private readonly Quaternion RIGHT_ROT = Quaternion.Euler(0.0f, 0.0f, -180.0f);
    private readonly Quaternion LEFT_ROT = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    private readonly Quaternion DOWN_ROT = Quaternion.Euler(0.0f, 0.0f, -270.0f);

    private Vector3 mDirection;
    private Quaternion mRotation;
    private Vector2 mSize = Vector3.one * MiniGameToader.TILE_STEP;
    private Rigidbody2D mBody;

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(transform.position - transform.right * MiniGameToader.TILE_STEP, Vector3.one * MiniGameToader.TILE_STEP);
    // }

    private void Start()
    {
		mBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetDirection(out mRotation, out mDirection);
        transform.rotation = mRotation;

        // Vector3 step = transform.right * MiniGameToader.TILE_STEP;
        // if (!Physics2D.OverlapBox(transform.position - step, mSize, 0.0f))
        // {
        // }

		mBody.MovePosition(transform.position + mDirection * MiniGameToader.TILE_STEP);
        //transform.Translate(mDirection * MiniGameToader.TILE_STEP, Space.World);
    }

    private void GetDirection(out Quaternion rotation, out Vector3 direction)
    {
        float vInput = mVInput.GetAxisUp(Input.GetAxisRaw("Vertical"));
        float hInput = mHInput.GetAxisUp(Input.GetAxisRaw("Horizontal"));

        rotation = transform.rotation;
        direction = Vector2.zero;
        if (vInput > 0.0f)
        {
            rotation = UP_ROT;
            direction = Vector2.up;
        }
        else if (vInput < 0.0f)
        {
            rotation = DOWN_ROT;
            direction = Vector2.down;
        }
        else if (hInput > 0.0f)
        {
            rotation = RIGHT_ROT;
            direction = Vector2.right;
        }
        else if (hInput < 0.0f)
        {
            rotation = LEFT_ROT;
            direction = Vector2.left;
        }
    }
}

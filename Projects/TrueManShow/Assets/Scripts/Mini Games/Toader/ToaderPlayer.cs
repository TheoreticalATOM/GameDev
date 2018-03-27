using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.Input;
using SDE;

public class ToaderPlayer : MonoBehaviour
{
    public int MovingUpPoint = 4;
    public LayerMask FrontColliderCheckMask;
    private AxisUp mVInput = new AxisUp();
    private AxisUp mHInput = new AxisUp();

    public event System.Action OnKilled;
    public event System.Action<int> OnPointsAdded;
    private Rigidbody2D mBody;

    private Vector3 mOriginPoint;

    private void Awake()
    {
		mBody = GetComponent<Rigidbody2D>();
        mOriginPoint = transform.position;
    }

    private void Update()
    {
        MoveWithInput();
    }
    public void Move(Vector3 direction)
    {
        mBody.MovePosition(transform.position + direction * MiniGameToader.TILE_STEP);
    }

    private void MoveWithInput()
    {
        float vInput = mVInput.GetAxisUp(Input.GetAxisRaw("Vertical"));
        float hInput = mHInput.GetAxisUp(Input.GetAxisRaw("Horizontal"));

        if (vInput > 0.0f)
        {
            transform.rotation = MiniGameToader.UP_ROT;
            Move(Vector2.up);

            Collider2D frontCollider = Physics2D.OverlapBox(transform.position - transform.right * MiniGameToader.TILE_STEP, Vector2.one * MiniGameToader.TILE_STEP, 0.0f,  FrontColliderCheckMask);
            if(!frontCollider || frontCollider.isTrigger)
                OnPointsAdded.TryInvoke(MovingUpPoint);
        }
        else if (vInput < 0.0f)
        {
            transform.rotation = MiniGameToader.DOWN_ROT;
            Move(Vector2.down);
        }
        else if (hInput > 0.0f)
        {
            transform.rotation = MiniGameToader.RIGHT_ROT;
            Move(Vector2.right);
        }
        else if (hInput < 0.0f)
        {
            transform.rotation = MiniGameToader.LEFT_ROT;
            Move(Vector2.left);
        }
    }

    public void Respawn(int offset)
    {
        Vector3 spawnPos = mOriginPoint;
        spawnPos.x += offset * MiniGameToader.TILE_STEP;
        transform.position = spawnPos;
    }

    public void Kill()
    {
        OnKilled.TryInvoke();
    }
}

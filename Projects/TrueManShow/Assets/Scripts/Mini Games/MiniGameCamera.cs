using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MiniGameCamera : MonoBehaviour
{
    // _______________________________________________________
    // @ Inspector
    public MiniGameToader.GameView DefaultState;

    // _______________________________________________________
    // @ Data
    private Camera mCamera;

    // _______________________________________________________
    // @ Controls
    public void SetTarget(MiniGame.GameView view)
    {
        transform.SetParent(view.Target);
        mCamera.orthographicSize = view.Size;
        mCamera.backgroundColor = view.BackgroundColor;

        // set the parent and local position
        // using this allows for auto following if need be
        Vector3 pos = transform.localPosition;
        pos.x = pos.y = 0.0f;
        transform.localPosition = pos;
    }

    public void ClearTarget()
    {
        SetTarget(DefaultState);
    }

    // _______________________________________________________
    // @ Methods
    private void Awake()
    {
        mCamera = GetComponent<Camera>();
        ClearTarget();
    }
}

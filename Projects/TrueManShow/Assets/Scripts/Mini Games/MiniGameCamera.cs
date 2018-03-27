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
    // @ Getter
    public Transform Target { get; private set; }

    // _______________________________________________________
    // @ Controls
    public void SetTarget(MiniGame.GameView view)
    {
        Target = view.Target;
        mCamera.orthographicSize = view.Size;
        mCamera.backgroundColor = view.BackgroundColor;
        enabled = view.Follow;

        UpdatePosition();
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
        enabled = false;
    }
    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 displacement = Target.position - transform.position;
        transform.Translate(displacement.x, displacement.y, 0.0f);
    }
}

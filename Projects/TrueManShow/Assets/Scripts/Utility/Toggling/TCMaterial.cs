using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TCMaterial : TogglerComponent
{
    private MeshRenderer mRenderer;
    public Material MaterialA;
    public Material MaterialB;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    protected override void OnToggleA()
    {
        mRenderer.material = MaterialA;
    }

    protected override void OnToggleB()
    {
        mRenderer.material = MaterialB;

    }
}

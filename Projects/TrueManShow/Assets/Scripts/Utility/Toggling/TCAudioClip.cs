using UnityEngine;

public class TCAudioClip : TogglerComponent
{
    public AudioClipPlayer Source;
    public AudioClip ClipA;
    public AudioClip ClipB;
    protected override void OnToggleA() { Source.PlayClip(ClipA); }
    protected override void OnToggleB() { Source.PlayClip(ClipB); }
}
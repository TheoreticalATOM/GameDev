using Sirenix.OdinInspector;
//using UnityEngine;
//using SDE;

public class Toggler : SerializedMonoBehaviour
{
    private TogglerComponent[] mComponents;
    
    private void Awake()
    {
        mComponents = GetComponents<TogglerComponent>();
    }

    public void SetToggleState(bool value)
    {
        foreach (TogglerComponent toggler in mComponents)
            toggler.SetToggleState(value);
    }

    public void Toggle()
    {
        foreach (TogglerComponent toggle in mComponents)
            toggle.Toggle();
    }
    
    //#if UNITY_EDITOR
//    public GameObject TogglerCopy;
//    public bool AddMissingUniqueComponents;
//
//    [Button]
//    public void SearchForLegacyAndClearIt()
//    {
//        AnimationSwapper animL = GetComponent<AnimationSwapper>();
//        if (animL)
//        {
//            TCAnimation anim = gameObject.GetOrAddComponent<TCAnimation>();
//            anim.Animator = animL.Animator;
//            anim.AnimationA = animL.AnimationA;
//            anim.AnimationB = animL.AnimationB;
//            DestroyImmediate(animL);
//        }
//    }
//
//    private void OnValidate()
//    {
//        if (!TogglerCopy)
//            return;
//
//        TCAudioClip copyClip = TogglerCopy.GetComponent<TCAudioClip>();
//        TCAnimation copyAnim = TogglerCopy.GetComponent<TCAnimation>();
//        TCGeneric copyGen = TogglerCopy.GetComponent<TCGeneric>();
//        if (copyClip)
//        {
//            TCAudioClip thisClip = gameObject.GetOrAddComponent<TCAudioClip>();
//            thisClip.ClipA = copyClip.ClipA;
//            thisClip.ClipB = copyClip.ClipB;
//            if (AddMissingUniqueComponents)
//            {
//                AudioClipPlayer thisPlayer = gameObject.GetOrAddComponent<AudioClipPlayer>();
//                thisClip.Source = thisPlayer;
//                AudioClipPlayer copyPlayer = TogglerCopy.GetComponent<AudioClipPlayer>();
//                if (copyPlayer)
//                    thisPlayer.MinMaxPitch = copyPlayer.MinMaxPitch;
//            }
//        }
//
//        if (copyAnim)
//        {
//            TCAnimation thisAnim = gameObject.GetOrAddComponent<TCAnimation>();
//            thisAnim.AnimationA = copyAnim.AnimationA;
//            thisAnim.AnimationB = copyAnim.AnimationB;
//            thisAnim.Animator = gameObject.GetComponent<Animator>();
//        }
//
//        if (copyGen)
//        {
//            TCGeneric thisGen = gameObject.GetOrAddComponent<TCGeneric>();
//            thisGen.A = copyGen.A;
//            thisGen.B = copyGen.B;
//        }
//
//        TogglerCopy = null;
//    }
//#endif
}

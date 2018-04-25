using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Collider), typeof(Rigidbody))]
public class LidRemoval : MonoBehaviour
{
    public ScriptableAnimation OpenAnimation;
    public AudioClip OpenClip;
    public Collider ChildTrigger;
    public AudioClipPlayer ChildLidRemovalPlayer;
    
    public UnityEvent OnLidRemoved;
    
    private Animator mAnim;
    private Collider mPhysicsCollider;
    private Rigidbody mBody;
    
    
    private void Awake()
    {
        mPhysicsCollider = GetComponent<Collider>();
        mAnim = GetComponent<Animator>();
        mBody = GetComponent<Rigidbody>();
        
        mBody.isKinematic = true;
        mPhysicsCollider.enabled = false;
        ChildTrigger.isTrigger = true;
    }

    public void RemoveLid()
    {
        ChildLidRemovalPlayer.PlayClip(OpenClip);
        OpenAnimation.SetValue(mAnim);
    }

    public void OnLidRemovedAnimationComplete()
    {
        mAnim.enabled = false;

        mPhysicsCollider.enabled = true;
        ChildTrigger.enabled = false;
        mBody.isKinematic = false;
        
        OnLidRemoved.Invoke();
    }
}

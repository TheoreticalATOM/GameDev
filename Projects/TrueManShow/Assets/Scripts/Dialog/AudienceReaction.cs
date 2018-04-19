using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public enum EReactionTypes
{
    Haha, Aw, Yay
}


public class AudienceReaction : SerializedScriptableObject
{
    public AudioClip[] Reactions;
    public RuntimeSet ReactionSource;
//
//    public void PlayReaction(EReactionTypes type, System.Action onReactionComplete = null)
//    {
//        NarrativeSource source = ReactionSource.GetFirst<NarrativeSource>();
//        Assert.IsNotNull(source, name + " : has incorrect narrative runtime set");
//        
//        source.PlayOne();
//    }
//
//    private AudioClip GetRandomClip(EReactionTypes type)
//    {
//        if (!Reactions.ContainsKey(type))
//            return null;
//        
//        AudioClip[] clips = Reactions[type];
//    }
    
}

using System.Collections.Generic;
using SDE;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Audience Reaction", menuName = "Dialog/Audience Reaction", order = 0)]
public class AudienceReaction : SerializedScriptableObject
{
    public Dictionary<ScriptableEnum, AudioClip[]> Reactions;
    public RuntimeSet ReactionSource;
    
    public void PlayReaction(ScriptableEnum type)
    {
        NarrativeSource source = ReactionSource.GetFirst<NarrativeSource>();
        Assert.IsNotNull(source, name + " : has incorrect narrative runtime set");

        AudioClip clip = GetRandomClip(type);
        if(clip)
            source.PlayOneShotAudioClip(clip);
    }

    private AudioClip GetRandomClip(ScriptableEnum type)
    {
        return !Reactions.ContainsKey(type) ? null : Reactions[type].RandomValue();
    }
    
}

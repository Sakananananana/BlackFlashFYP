using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FadeEventChannelSO", menuName = "Scriptable Objects /Channels /FadeEventChannelSO")]
public class FadeEventChannelSO : ScriptableObject
{
    public UnityAction<bool, float> OnEventRaised;

    public void FadeIn(float duration)
    {
        Fade(true, duration); //fades into black
    }

    public void FadeOut(float duration)
    { 
        Fade(false, duration); //fades into game
    }

    public void Fade(bool fadeIn, float duration)
    { 
        if(OnEventRaised != null)
            OnEventRaised.Invoke(fadeIn, duration);
    }
}

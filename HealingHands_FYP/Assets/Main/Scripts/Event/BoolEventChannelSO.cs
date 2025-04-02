using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Scriptable Objects /Channels /BoolEventChannelSO")]
public class BoolEventChannelSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool val)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(val);
    }
}

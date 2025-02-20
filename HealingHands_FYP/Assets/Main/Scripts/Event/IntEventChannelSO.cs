using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntChannelSO", menuName = "Scriptable Objects /Channels /IntChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int index)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(index);
    }
}

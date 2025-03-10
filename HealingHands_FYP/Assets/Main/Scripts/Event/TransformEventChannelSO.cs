using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TransformEventChannelSO", menuName = "Scriptable Objects /Channels /TransformEventChannelSO")]
public class TransformEventChannelSO : ScriptableObject
{
    public UnityAction<Transform> OnEventRaised;

    public void RaiseEvent(Transform target)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(target);
    }
}

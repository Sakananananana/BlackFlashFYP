using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ColliderEventChannelSO", menuName = "Scriptable Objects /Channels /ColliderEventChannelSO")]
public class ColliderEventChannelSO : ScriptableObject
{
    public UnityAction<BoxCollider2D> OnEventRaised;

    public void RaiseEvent(BoxCollider2D col)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(col);
    }
}

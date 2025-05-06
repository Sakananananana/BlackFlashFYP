using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BossEventChannelSO", menuName = "Scriptable Objects /Channels /BossEventChannelSO")]
public class BossEventChannelSO : ScriptableObject
{
    public UnityAction<BossHealthUIItem> OnEventRaised;

    public void RaiseEvent(BossHealthUIItem boss)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(boss);
    }
}

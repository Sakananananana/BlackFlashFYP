using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadEventChannelSO", menuName = "Scriptable Objects /Channels /LoadEventChannelSO")]
public class LoadEventChannelSO : ScriptableObject
{
    public UnityAction<GameSceneSO> OnLoadingRequested;

    public void RaiseEvent(GameSceneSO locationToLoad)
    {
        if (OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(locationToLoad);
        }
    }
}

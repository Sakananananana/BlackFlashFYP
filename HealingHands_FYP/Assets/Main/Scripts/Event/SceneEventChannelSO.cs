using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneEventChannelSO", menuName = "Scriptable Objects /Channels /SceneEventChannelSO")]
public class SceneEventChannelSO : ScriptableObject
{
    public UnityAction<GameSceneSO.GameSceneType> OnEventRaised;

    public void RaiseEvent(GameSceneSO.GameSceneType sceneType)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(sceneType);
    }
}

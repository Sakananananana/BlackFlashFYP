using System.Collections.Generic;
using UnityEngine;

public class SceneHistoryManager : MonoBehaviour
{
    public static SceneHistoryManager Instance;

    private Stack<GameSceneSO> _sceneHistory = new Stack<GameSceneSO>();

    public void RecordScene(GameSceneSO scene)
    {
        _sceneHistory.Push(scene);
    }

    public GameSceneSO GetPreviousScene()
    {
        if (_sceneHistory.Count > 0)
            return _sceneHistory.Pop();

        return null;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


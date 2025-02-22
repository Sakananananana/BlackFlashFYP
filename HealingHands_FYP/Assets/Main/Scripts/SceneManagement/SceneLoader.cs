using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO _sceneToLoad;
    [SerializeField] public List<GameSceneSO> _compulsoryScene;
    //private List<Scene> _scenes;

    void Awake()
    {
        if (_compulsoryScene != null)
        {
            for (int i = 0; i < _compulsoryScene.Count; i++)
            {
                //if (SceneManager.GetActiveScene() != _compulsoryScene[i].sceneReference)
                //{
                //    SceneManager.LoadSceneAsync(_compulsoryScene[i].name, LoadSceneMode.Additive);
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadGameplay(GameSceneSO scene)
    { 
        //if ()
    }
}

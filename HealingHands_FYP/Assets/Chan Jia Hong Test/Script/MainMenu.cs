using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Mode, mainmenu, options;


    // Start is called before the first frame update
    public void Start()
    {
        mainmenu.SetActive(true);
        Mode.SetActive(false);
        options.SetActive(false);
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void mode1()
    {
        mainmenu.SetActive(false);
        Mode.gameObject.SetActive(true);

    }
    public void option()
    {
        mainmenu.SetActive(false);
        options.gameObject.SetActive(true);

    }
    public void back()
    {
        mainmenu.SetActive(true);
        Mode.SetActive(false);
        options.SetActive(false);
    }
}


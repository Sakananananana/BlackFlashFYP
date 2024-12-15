using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainmenu, options;
    [SerializeField] GameObject[] firstButtons;
    [SerializeField] GameObject[] arrow;

    [SerializeField] AudioData _selectedAudio;
    [SerializeField] AudioConfiguration _audioConfig;

    // Start is called before the first frame update
    public void Start()
    {
        mainmenu.SetActive(true);
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

    public void Option()
    {
        mainmenu.SetActive(false);
        options.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButtons[1]);
    }

    public void Back()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstButtons[0]);
    }

    public void Select1() 
    { 
        arrow[0].gameObject.SetActive(true);
        arrow[1].gameObject.SetActive(false);
        arrow[2].gameObject.SetActive(false);
    }

    public void Select2()
    {
        arrow[0].gameObject.SetActive(false);
        arrow[1].gameObject.SetActive(true);
        arrow[2].gameObject.SetActive(false);
    }

    public void Select3()
    {
        arrow[0].gameObject.SetActive(false);
        arrow[1].gameObject.SetActive(false);
        arrow[2].gameObject.SetActive(true);
    }

    public void Select4()
    {
        arrow[3].gameObject.SetActive(true);
        arrow[4].gameObject.SetActive(false);
    }

    public void Select5()
    {
        arrow[3].gameObject.SetActive(false);
        arrow[4].gameObject.SetActive(true);
    }

    public void OnSelect()
    {
        AudioManager.Instance.PlayRaisedAudio(_selectedAudio, _audioConfig);
    }
}


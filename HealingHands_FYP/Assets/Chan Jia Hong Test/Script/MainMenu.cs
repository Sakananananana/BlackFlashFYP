using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainmenu, options;
    [SerializeField] GameObject[] firstButtons;
    [SerializeField] GameObject[] arrow;

    [SerializeField] AudioChannelSO _audioChannelSO;
    [SerializeField] AudioData _selectedAudio;
    [SerializeField] AudioConfiguration _audioConfig;

    // Start is called before the first frame update
    public void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
    }

    public void LoadLevel()
    {
        if (TutorialManager.instance.IsTutorialCompleted() == true)
        {
            SceneManager.LoadScene("Village");
        }
        else 
        {
            SceneManager.LoadScene("Tutorial");
            TutorialManager.instance.OnTutorialComplete();
        }
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
        _audioChannelSO.OnAudioPlayRequested(_selectedAudio, _audioConfig);
    }
}


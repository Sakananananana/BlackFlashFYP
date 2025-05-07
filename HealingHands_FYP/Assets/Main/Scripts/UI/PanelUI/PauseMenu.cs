using PlayerInputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _firstButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] arrow;

    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        arrow[0].gameObject.SetActive(false);
        arrow[1].gameObject.SetActive(true);
        arrow[2].gameObject.SetActive(false);

    }
    public void PauseGame()
    {
        EventSystem.current.SetSelectedGameObject(_firstButton);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
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
}

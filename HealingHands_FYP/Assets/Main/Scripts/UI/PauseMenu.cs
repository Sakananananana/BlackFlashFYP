using PlayerInputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]private InputReader _inputReader;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] arrow;
    private bool isGamePaused;

    private void OnEnable()
    {
        _inputReader.PauseEvent += PauseGame;
    }
    private void OnDisable()
    {
        _inputReader.PauseEvent -= PauseGame;
    }
    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        arrow[0].gameObject.SetActive(false);
        arrow[1].gameObject.SetActive(true);
        arrow[2].gameObject.SetActive(false);

    }
    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;

    }
    public void ContinueGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    // Update is called once per frame
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (isGamePaused)
    //        {
    //            ContinueGame();
    //            Debug.Log("Game should NOT be paused rn");
    //        }
    //        else
    //        {
    //            PauseGame();
    //            Debug.Log("Game should be paused rn");
    //        }
    //    }
    //}
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
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

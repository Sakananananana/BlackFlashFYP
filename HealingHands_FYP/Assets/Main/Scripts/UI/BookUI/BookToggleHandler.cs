using UnityEngine;
using PlayerInputSystem;


public class BookToggleHandler : MonoBehaviour
{
    [SerializeField] private GameObject bookUI; // Reference to the book UI (parent of pages)
    [SerializeField] private BookManager bookManager; // Your BookManager script
    [SerializeField] private InputReader inputReader;

    private bool isBookOpen = false;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K) && !isBookOpen)
    //    {
    //        OpenBook();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Escape) && isBookOpen)
    //    {
    //        CloseBook();
    //    }
    //}
    private void OnEnable()
    {
        inputReader.OpenBook += OpenBook;
    }

    private void OnDisable()
    {
        inputReader.OpenBook -= OpenBook;
    }

    private void OpenBook()
    {
        bookUI.SetActive(true);
        bookManager.ShowPage(0);
        bookManager.RefreshAllPages();
        isBookOpen = true;
        inputReader.CloseBook += CloseBook;

        // Optional: pause game
        Time.timeScale = 0f;
    }

    private void CloseBook()
    {
        Debug.Log("Close");
        bookUI.SetActive(false);
        isBookOpen = false;
        inputReader.CloseBook -= CloseBook;
        // Resume game
        Time.timeScale = 1f;
    }
}

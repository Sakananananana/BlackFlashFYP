using UnityEngine;
using PlayerInputSystem;
using Inventory.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    //All the User Interfaces
    [SerializeField] private UIInventoryPage _inventoryPanel;
    [SerializeField] private PauseMenu _pauseMenu;

    private void OnEnable()
    {
        _inputReader.OpenInventoryEvent += OpenInventoryScreen;
        _inputReader.PauseEvent += Pause;
    }

    private void OnDisable()
    {
        _inputReader.OpenInventoryEvent -= OpenInventoryScreen;
        _inputReader.PauseEvent -= Pause;
    }

    void OpenInventoryScreen()
    {
        _inputReader.CloseInventoryEvent += CloseInventoryScreen;
        _inputReader.SetInventory();

        //if crafting deh deh deh
        Time.timeScale = 0;

        _inventoryPanel.FillInventory();
        _inventoryPanel.gameObject.SetActive(true);
    }

    void CloseInventoryScreen()
    {
        _inputReader.CloseInventoryEvent -= CloseInventoryScreen;
        _inputReader.SetGameplay();

        Time.timeScale = 1;
        _inventoryPanel.CloseInventory();
    }

    void Pause()
    {
        _inputReader.ResumeEvent += Resume;

        _inputReader.SetUI();
        _pauseMenu.PauseGame(); 
    }

    void Resume()
    {
        _inputReader.ResumeEvent -= Resume;

        _pauseMenu.ContinueGame();
        _inputReader.SetGameplay();
    }
}

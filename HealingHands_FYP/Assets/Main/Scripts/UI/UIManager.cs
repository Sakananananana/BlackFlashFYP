using UnityEngine;
using PlayerInputSystem;
using Inventory.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private BoolEventChannelSO _onCraftingStarted;

    //All the User Interfaces
    [SerializeField] private UIInventoryPage _inventoryPanel;
    [SerializeField] private PauseMenu _pauseMenu;

    bool _isCrafting = false;

    private void OnEnable()
    {
        _onCraftingStarted.OnEventRaised += OpenInventoryForCrafting;

        _inputReader.OpenInventoryEvent += OpenInventoryScreen;
        _inputReader.PauseEvent += OpenSettingScreen;
    }

    private void OnDisable()
    {
        _onCraftingStarted.OnEventRaised -= OpenInventoryForCrafting;

        _inputReader.OpenInventoryEvent -= OpenInventoryScreen;
        _inputReader.PauseEvent -= OpenSettingScreen;
    }

    void OpenInventoryForCrafting(bool val)
    { 
        _isCrafting = val;
    }

    void OpenInventoryScreen()
    {
        _inputReader.CloseInventoryEvent += CloseInventoryScreen;
        _inputReader.SetInventory();

        Time.timeScale = 0;

        if (_isCrafting)
        {
            _inventoryPanel.FillInventory();
            _inventoryPanel.SetInspector(InspectorType.Crafting);
        }
        else
        {
            _inventoryPanel.FillInventory();
            _inventoryPanel.SetInspector();
        }
        
        _inventoryPanel.gameObject.SetActive(true);
    }

    void CloseInventoryScreen()
    {
        _inputReader.CloseInventoryEvent -= CloseInventoryScreen;
        _inputReader.SetGameplay();

        Time.timeScale = 1;

        _inventoryPanel.CloseInventory();
    }

    void OpenSettingScreen()
    {
        _inputReader.ResumeEvent += CloseSettingScreen;

        _inputReader.SetUI();
        _pauseMenu.PauseGame(); 
    }

    void CloseSettingScreen()
    {
        _inputReader.ResumeEvent -= CloseSettingScreen;

        _pauseMenu.ContinueGame();
        _inputReader.SetGameplay();
    }
}

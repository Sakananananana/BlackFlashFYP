using UnityEngine;
using PlayerInputSystem;
using Inventory.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    //All the User Interfaces
    [SerializeField] private UIInventoryPage _inventoryPanel;

    private void OnEnable()
    {
        _inputReader.OpenInventoryEvent += OpenInventoryScreen;
    }

    private void OnDisable()
    {
        _inputReader.OpenInventoryEvent -= OpenInventoryScreen;
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
}

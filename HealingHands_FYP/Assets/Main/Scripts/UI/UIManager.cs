using UnityEngine;
using PlayerInputSystem;
using Inventory.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private BoolEventChannelSO _onCraftingStarted;
    [SerializeField] private BoolEventChannelSO _onShoppingStarted;

    //All the User Interfaces
    [SerializeField] private UIInventoryPage _inventoryPanel;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private TaskUIManager _taskUIManager;

    bool _isCrafting = false;
    bool _isShopping = false;

    private void OnEnable()
    {
        _onCraftingStarted.OnEventRaised += OpenInventoryForCrafting;
        //_onShoppingStarted.OnEventRaised += OpenInventoryForShopping;

        _inputReader.OpenInventoryEvent += OpenInventoryScreen;
        _inputReader.PauseEvent += OpenSettingScreen;
        _inputReader.InteractEvent += OpenShopScreen;

    }

    private void OnDisable()
    {
        _onCraftingStarted.OnEventRaised -= OpenInventoryForCrafting;
        //_onShoppingStarted.OnEventRaised -= OpenInventoryForShopping;

        _inputReader.OpenInventoryEvent -= OpenInventoryScreen;
        _inputReader.PauseEvent -= OpenSettingScreen;
        _inputReader.InteractEvent -= OpenShopScreen;
    }

    void OpenInventoryForCrafting(bool val)
    { 
        _isCrafting = val;
    }

    //void OpenInventoryForShopping(bool val)
    //{
    //    _isShopping = val;

    //    if (val)
    //        OpenShopScreen();
    //}

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
        //Debug.Log("Closing shop screen");
    }

    void OpenShopScreen()
    {

        _inputReader.ResumeEvent += CloseShopScreen;
        _inputReader.SetUI();

        Time.timeScale = 0;
        _shopManager.OpenShop();
    }

    void CloseShopScreen()
    {
        _inputReader.ResumeEvent -= CloseShopScreen;
        _shopManager.CloseShop();

        Time.timeScale = 1;
        _inputReader.SetGameplay();
    }

}

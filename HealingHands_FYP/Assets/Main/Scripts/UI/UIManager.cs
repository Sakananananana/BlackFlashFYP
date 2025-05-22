using UnityEngine;
using PlayerInputSystem;
using Inventory.UI;
using UnityEngine.SceneManagement;
using System.Linq;
//using UnityEditor.EditorTools;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private BoolEventChannelSO _onCraftingStarted;
    [SerializeField] private BoolEventChannelSO _onShoppingStarted;
    [SerializeField] private BoolEventChannelSO _onWeaponShoppingStarted;
    [SerializeField] private BoolEventChannelSO _onWeaponStarted;
    [SerializeField] private BoolEventChannelSO _onOpenRoom;
    [SerializeField] private InterectionManager _interactionManager;

    //In Game Screen UIs
    [SerializeField] private BoolEventChannelSO _setInGameScreenUI;
    [SerializeField] private SceneEventChannelSO _onSceneChanges;
    [SerializeField] private BoolEventChannelSO _isCoinsBelowWarpCost;
    [SerializeField] private BoolEventChannelSO _isInCombat;

    //All the User Interfaces
    [SerializeField] private UIInventoryPage _inventoryPanel;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private WeaponUpgradeButton _weaponManager;
    [SerializeField] private TaskUIManager _taskUIManager;
    [SerializeField] private GameObject _roomUIPanel;
    [SerializeField] private GameObject _shopUIPanel;
    [SerializeField] private GameObject _craftingUIPanel;
    [SerializeField] private GameObject bookUI; // Reference to the book UI (parent of pages)
    [SerializeField] private BookManager bookManager; // Your BookManager script
    [SerializeField] private InGameScreenManager _inGameScreenUI;
    [SerializeField] private GameObject _damageScreen;

    private bool _locationaCanWarp = false;
    private bool _isBelowWarpCost = false;
    private bool _inCombat = false;

    bool _isCrafting = false;
    bool _isShopping = false;


    private void OnEnable()
    {
        _onCraftingStarted.OnEventRaised += OpenInventoryForCrafting;
        _onShoppingStarted.OnEventRaised += OnShopRequested;
        _onWeaponShoppingStarted.OnEventRaised += OnWeaponShopRequested;

        _inputReader.OpenInventoryEvent += OpenInventoryScreen;
        _inputReader.PauseEvent += OpenSettingScreen;
        _inputReader.OpenBook += OpenBook;
        _inputReader.CloseBook += CloseBook;

        //In Game Screen UI Handler
        _setInGameScreenUI.OnEventRaised += SetInGameScreenUI;
        _onSceneChanges.OnEventRaised += OnLocationChange;
        _isInCombat.OnEventRaised += IsInCombatHandler;
        _isCoinsBelowWarpCost.OnEventRaised += IsBelowWarpThreshold;
    }

    private void OnDisable()
    {
        _onCraftingStarted.OnEventRaised -= OpenInventoryForCrafting;
        _onShoppingStarted.OnEventRaised -= OnShopRequested;
        _onWeaponShoppingStarted.OnEventRaised -= OnWeaponShopRequested;

        _inputReader.OpenInventoryEvent -= OpenInventoryScreen;
        _inputReader.PauseEvent -= OpenSettingScreen;
        _inputReader.OpenBook -= OpenBook;
        _inputReader.CloseBook -= CloseBook;

        //In Game Screen UI Handler
        _setInGameScreenUI.OnEventRaised -= SetInGameScreenUI;
        _onSceneChanges.OnEventRaised -= OnLocationChange;
        _isInCombat.OnEventRaised -= IsInCombatHandler;
        _isCoinsBelowWarpCost.OnEventRaised -= IsBelowWarpThreshold;
    }

    void OpenInventoryForCrafting(bool val)
    { 
        _isCrafting = val;
        if (_craftingUIPanel != null)
        {
            _craftingUIPanel.SetActive(val);
        }
    }

    void OnWeaponShopRequested(bool val)
    {
        if (val == true)
        _inputReader.InteractEvent += OpenWeaponScreen;
        else
        _inputReader.InteractEvent -= OpenWeaponScreen;


    }

    //shop for player to sell things
    void OnShopRequested(bool val)
    {

        if (val == true)
        {
            _inputReader.InteractEvent += OpenShopScreen;
            if (_roomUIPanel != null)
            {
                _roomUIPanel.SetActive(val);
            }
        }
        else
        {
            _inputReader.InteractEvent -= OpenShopScreen;
            if (_roomUIPanel != null)
            {
                _roomUIPanel.SetActive(val);
            }
        }
    }

    void OpenInventoryForShopping(bool val)
    {
        _isShopping = val;

        if (val)
            OpenShopScreen();
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

    void OpenWeaponScreen()
    {

        _inputReader.ResumeEvent += CloseWeaponScreen;
        _inputReader.SetUI();

        Time.timeScale = 0;
        _weaponManager.OpenWeapon();
    }

    void CloseWeaponScreen()
    {
        _inputReader.ResumeEvent -= CloseWeaponScreen;
        _weaponManager.CloseWeapon();

        Time.timeScale = 1;
        _inputReader.SetGameplay();
    }

    private void OpenBook()
    {
        bookUI.SetActive(true);
        bookManager.ShowPage(0);
        bookManager.RefreshAllPages();

        _inputReader.ResumeEvent += CloseBook;
        _inputReader.SetUI();
        // Optional: pause game
        Time.timeScale = 0f;
    }

    private void CloseBook()
    {
        Debug.Log("Close");
        bookUI.SetActive(false);

        _inputReader.ResumeEvent -= CloseBook;
        _inputReader.SetGameplay();
        // Resume game
        Time.timeScale = 1f;
    }

    private void SetInGameScreenUI(bool val) => _inGameScreenUI.gameObject.SetActive(val);

    private void OnLocationChange(GameSceneSO.GameSceneType sceneType)
    {
        _damageScreen.SetActive(false);
        if (sceneType == GameSceneSO.GameSceneType.Location_BossRoom || sceneType == GameSceneSO.GameSceneType.Location_Dungeon)
        {
            _inGameScreenUI.SetDungeonUIScreen();
            _locationaCanWarp = true;
            SetReturnButton();
        }
        else if (sceneType == GameSceneSO.GameSceneType.Location_Village)
        {
            _inGameScreenUI.SetVillageUIScreen();
            _locationaCanWarp = false;
        }
        else
        { _locationaCanWarp = false; }
    }

    private void IsInCombatHandler(bool val)
    {
        _inCombat = val;
        SetReturnButton();
    }

    private void IsBelowWarpThreshold(bool val)
    {
        _isBelowWarpCost = val;
        SetReturnButton();
    }

    private void SetReturnButton()
    {
        _inGameScreenUI.WarppableCheck(_inCombat, _isBelowWarpCost, _locationaCanWarp);
    }
}

using UnityEngine;

public class InGameScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _goldAmountUI;
    [SerializeField] private GameObject _healthBarUI;

    [SerializeField] private SceneEventChannelSO _onSceneChanges;
    [SerializeField] private BoolEventChannelSO _isCoinsBelowWarpCost;
    [SerializeField] private BoolEventChannelSO _isInCombat;
    [SerializeField] private ButtonUI _returnButtonUI;
    private bool _isBelowWarpCost = false;
    private bool _isCurrentlyInCombat = false;

    private void OnEnable()
    {
        _isInCombat.OnEventRaised += IsInCombatHandler;
        _isCoinsBelowWarpCost.OnEventRaised += IsBelowWarpThreshold;

        _onSceneChanges.OnEventRaised += OnLocationChange;
    }

    private void OnDisable()
    {
        _isInCombat.OnEventRaised -= IsInCombatHandler;
        _isCoinsBelowWarpCost.OnEventRaised -= IsBelowWarpThreshold;

        _onSceneChanges.OnEventRaised -= OnLocationChange;
    }

    public void SetDungeonUIScreen()
    {
        _healthBarUI.SetActive(true);
        _returnButtonUI.gameObject.SetActive(true);
    }

    public void SetVillageUIScreen()
    {
        _healthBarUI.SetActive(false);
        _returnButtonUI.gameObject.SetActive(false);
    }

    private void IsInCombatHandler(bool val) 
    {
        _isCurrentlyInCombat = val;
        WarppableCheck();
    }

    private void IsBelowWarpThreshold (bool val)
    {
        _isBelowWarpCost = val;
        WarppableCheck();
    }

    private void WarppableCheck()
    {
        if(_isCurrentlyInCombat == false && _isBelowWarpCost == false)
            _returnButtonUI.gameObject.SetActive(true);
        else
            _returnButtonUI.gameObject.SetActive(false);

        //Debug.Log($"from In Game Screen, IsCoinsBelowWarpCost: {_isBelowWarpCost}");
    }

    private void OnLocationChange(GameSceneSO.GameSceneType sceneType)
    {
            switch (sceneType)
            {
                case GameSceneSO.GameSceneType.Location_Dungeon:
                    {
                        SetDungeonUIScreen();
                        WarppableCheck();
                        break;
                    }

                case GameSceneSO.GameSceneType.Location_BossRoom:
                    {
                        SetDungeonUIScreen();
                        WarppableCheck();
                        break;
                    }

                case GameSceneSO.GameSceneType.Location_Village:
                    {
                        SetVillageUIScreen();
                        break;
                    }
            }
     
    }
}

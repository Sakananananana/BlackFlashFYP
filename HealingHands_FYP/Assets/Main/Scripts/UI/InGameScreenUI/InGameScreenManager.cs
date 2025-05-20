using UnityEngine;

public class InGameScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private ButtonUI _returnButtonUI;

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

    public void WarppableCheck(bool inCombat, bool isBelowWarpCost, bool locationCanWarp)
    {
        if(inCombat == false && isBelowWarpCost == false && locationCanWarp == true)
            _returnButtonUI.gameObject.SetActive(true);
        else
            _returnButtonUI.gameObject.SetActive(false);
    }
}

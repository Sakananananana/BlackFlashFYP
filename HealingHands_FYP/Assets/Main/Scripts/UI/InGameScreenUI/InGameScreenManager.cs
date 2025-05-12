using UnityEngine;

public class InGameScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _goldAmountUI;
    [SerializeField] private GameObject _healthBarUI;

    [SerializeField] private BoolEventChannelSO _isCoinsBelowWarpCost;
    [SerializeField] private ButtonUI _returnButtonUI;
    private bool _isBelowWarpCost;

    private void OnEnable()
    {
        _isCoinsBelowWarpCost.OnEventRaised += SetWarpButton;
    }

    private void OnDisable()
    {
        _isCoinsBelowWarpCost.OnEventRaised -= SetWarpButton;
    }

    public void SetCombatUIScreen()
    { 
        if(_isBelowWarpCost == false)
        _returnButtonUI.gameObject.SetActive(!_returnButtonUI.gameObject.activeSelf);
    }

    public void SetDungeonUIScreen()
    {
        _healthBarUI.SetActive(true);

        if (_isBelowWarpCost == false)
        _returnButtonUI.gameObject.SetActive(true);
    }

    public void SetVillageUIScreen()
    {
        _healthBarUI.SetActive(false);
        _returnButtonUI.gameObject.SetActive(false);
    }

    private void SetWarpButton(bool val) 
    {
        _isBelowWarpCost = val;
        _returnButtonUI.gameObject.SetActive(val);
    }
}

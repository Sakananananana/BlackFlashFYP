using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryAction : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _useButton;

    [Header("Listening to...")]
    [SerializeField] private IntEventChannelSO _onSlotEventRaised;

    [Header("Broadcasting to...")]
    [SerializeField] private IntEventChannelSO _onSlotEventEnded;
    [SerializeField] private IntEventChannelSO _onItemDrop;
    [SerializeField] private IntEventChannelSO _onItemUse;

    private int _currentSelectedSlot = -1;

    private void OnEnable()
    {
        _onSlotEventRaised.OnEventRaised += OnSlotEventRaised;
    }

    private void OnDisable()
    {
        _onSlotEventRaised.OnEventRaised -= OnSlotEventRaised; 
    }

    public void OnUseButtonPressed()
    {
        _onItemUse.OnEventRaised?.Invoke(_currentSelectedSlot);
        OnSlotEventEnded();
    }

    public void OnDropButtonPressed()
    {
        _onItemDrop.OnEventRaised?.Invoke( _currentSelectedSlot);
        OnSlotEventEnded();
    }

    public void OnSlotEventEnded()
    {
        _onSlotEventEnded.OnEventRaised?.Invoke(_currentSelectedSlot);
        _currentSelectedSlot = -1;
    }

    public void OnSlotEventRaised(int index)
    {
        _currentSelectedSlot = index;
    }

    public void OpenActionPanel()
    {
        _content.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_useButton);        
    }

    public void CloseActionPanel()
    {
        if (_content.activeSelf)
        {
            _content.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryAction : MonoBehaviour
    {
        [Header("Action Tab Buttons")]
        [SerializeField] private List<GameObject> _buttonList = new List<GameObject>();
        private List<GameObject> _enableButtonList = new List<GameObject>();
        [SerializeField] private GameObject _content;

        [Header("Listening to...")]
        [SerializeField] private IntEventChannelSO _onSlotEventRaised;

        [Header("Broadcasting to...")]
        [SerializeField] private IntEventChannelSO _onSlotEventEnded;
        [SerializeField] private IntEventChannelSO _onItemRemove;
        [SerializeField] private IntEventChannelSO _onItemDrop;
        [SerializeField] private IntEventChannelSO _onItemUse;
        [SerializeField] private IntEventChannelSO _onItemAdd;

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
            _onItemDrop.OnEventRaised?.Invoke(_currentSelectedSlot);
            OnSlotEventEnded();
        }

        public void OnAddButtonPressed()
        {
            _onItemAdd.OnEventRaised?.Invoke(_currentSelectedSlot);
            OnSlotEventEnded();
        }

        public void OnRemoveButtonPressed()
        {  
            _onItemRemove.OnEventRaised?.Invoke(_currentSelectedSlot);
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

        public void OpenActionPanel(BtnInteractionType type = BtnInteractionType.Normal)
        {
            _content.SetActive(true);

            AddActionButton(type);
            EventSystem.current.SetSelectedGameObject(_enableButtonList[0]);
        }

        public void CloseActionPanel()
        {
            if (_content.activeSelf)
            {
                _content.SetActive(false);

                RemoveActionButton();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void AddActionButton(BtnInteractionType type)
        {
            switch (type)
            {
                case BtnInteractionType.Normal:
                    {
                        _enableButtonList.Add(_buttonList[2]);
                        _enableButtonList.Add(_buttonList[3]);
                        _enableButtonList.Add(_buttonList[4]);
                        break;
                    }

                case BtnInteractionType.Craft_Add:
                    {
                        _enableButtonList.Add(_buttonList[0]);
                        _enableButtonList.Add(_buttonList[4]);
                        break; 
                    }

                case BtnInteractionType.Craft_Remove: 
                    {
                        _enableButtonList.Add(_buttonList[1]);
                        _enableButtonList.Add(_buttonList[4]);
                        break;
                    }
            }

            for (int i = 0; i < _enableButtonList.Count; i++)
            {
                _enableButtonList[i].SetActive(true);
            }
        }

        private void RemoveActionButton()
        {
            for (int i = 0; i < _enableButtonList.Count; i++)
            {
                _enableButtonList[i].SetActive(false);
            }

            _enableButtonList.Clear();
        }
    }
}

using PlayerInputSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum InteractionType { None, PickUp, Craft,Shop,WeaponShop}

public class Interaction 
{
    public InteractionType _type;
    public GameObject _interactableObject;

    public Interaction(InteractionType t, GameObject obj)
    {
        _type = t;
        _interactableObject = obj;
    }
}

public class InterectionManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BoolEventChannelSO _onCraftingStarted;
    [SerializeField] private BoolEventChannelSO _onShoppingStarted;
    [SerializeField] private BoolEventChannelSO _onWeaponStarted;
    [SerializeField] private BoolEventChannelSO _interactionEvent;

    private LinkedList<Interaction> _interactable = new LinkedList<Interaction>();
    public InteractionType _currentInteraction;

    public void OnTriggerChangeDetected(bool entered, GameObject obj)
    {
        if (entered)
            AddPotentialInteraction(obj);
        else
            RemovePotentialInteraction(obj);
    }

    private void AddPotentialInteraction(GameObject obj)
    {
        Interaction newPotentialInteraction = new Interaction(InteractionType.None, obj);

        if (obj.CompareTag("Pickable"))
        {
            newPotentialInteraction._type = InteractionType.PickUp;
        }
        else if (obj.CompareTag("CraftingPlace"))
        {
            newPotentialInteraction._type = InteractionType.Craft;
            _onCraftingStarted.RaiseEvent(true);
        }
        else if (obj.CompareTag("Shop"))
        {
            newPotentialInteraction._type = InteractionType.Shop;
            _interactionEvent.RaiseEvent(true);
            _onShoppingStarted.RaiseEvent(true);
        }
        else if (obj.CompareTag("WeaponShop"))
        {
            Debug.Log("Weapon shop interaction entered");
            newPotentialInteraction._type = InteractionType.WeaponShop;
            _interactionEvent.RaiseEvent(true);
            _onWeaponStarted.RaiseEvent(true);
        }

        if (newPotentialInteraction._type != InteractionType.None)
        {
            _interactable.AddFirst(newPotentialInteraction);
        }
    }

    private void RemovePotentialInteraction(GameObject obj)
    {
        LinkedListNode<Interaction> currentNode = _interactable.First;
        while (currentNode != null) 
        {
            if (currentNode.Value._interactableObject == obj)
            {
                if (currentNode.Value._type == InteractionType.Craft)
                { _onCraftingStarted.RaiseEvent(false); }

                if (currentNode.Value._type == InteractionType.Shop)
                { _onShoppingStarted.RaiseEvent(false);
                    _interactionEvent.RaiseEvent(false);
                }
                if (currentNode.Value._type == InteractionType.WeaponShop)
                {
                    _onWeaponStarted.RaiseEvent(false);
                    _interactionEvent.RaiseEvent(false);
                }
                _interactable.Remove(currentNode);
                break;
            }

            currentNode = currentNode.Next;
        }
    }

    private void ResetPotentialInteractions()
    {
        _interactable.Clear();
    }
}

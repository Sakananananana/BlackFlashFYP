using UnityEngine;
using PlayerInputSystem;

public class House : Triggerable
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _interactionButton;

    [Header("Broadcasting on...")]
    [SerializeField] private VoidEventChannelSO _onCrafting;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Player"))
        {
            _interactionButton.SetActive(true);
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);

       
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        _interactionButton.SetActive(false);
    }

    private void EnableCrafting()
    { 
        _inputReader.InteractEvent -= EnableCrafting;
        _onCrafting.RaiseEvent();
    }
}

using UnityEngine;

public class Triggerable : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _interactionEvent;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        _interactionEvent.OnEventRaised(true);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    { 
        _interactionEvent.OnEventRaised(false);
    }
}

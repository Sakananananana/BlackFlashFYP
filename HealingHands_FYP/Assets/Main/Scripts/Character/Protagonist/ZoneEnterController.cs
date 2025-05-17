using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> { }

public class ZoneEnterController : MonoBehaviour
{
    [SerializeField] private BoolEvent _enterZone;
    [SerializeField] private LayerMask _layers = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(true, other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(false, other.gameObject);
        }
    }
}

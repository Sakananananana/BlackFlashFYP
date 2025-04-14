using UnityEngine;

//reusable script can be attached to enemy as well.
public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected AttackConfigSO _attackConfig;

    [Header("Broadcasting on...")]
    [SerializeField] protected VoidEventChannelSO _raiseCamShake;

    protected abstract void OnTriggerEnter2D(Collider2D other);
}

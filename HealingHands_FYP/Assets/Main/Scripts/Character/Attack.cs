using UnityEngine;

//reusable script can be attached to enemy as well.
public class Attack : MonoBehaviour
{
    [SerializeField] private AttackConfigSO _attackConfig;

    [Header("Broadcasting on...")]
    [SerializeField] private VoidEventChannelSO _raiseCamShake;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                damageable.RecieveDamage(_attackConfig.AttackDamage, dir);

                //move to state action later
                _raiseCamShake?.RaiseEvent();
            }   
        }
    }
}

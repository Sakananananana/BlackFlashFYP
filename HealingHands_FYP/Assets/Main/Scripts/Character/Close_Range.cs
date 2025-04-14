using UnityEngine;

public class Close_Range : Attack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageable))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                damageable.RecieveAttack(_attackConfig.AttackDamage, dir);

                //move to state action later
                _raiseCamShake?.RaiseEvent();
            }
        }
    }
}

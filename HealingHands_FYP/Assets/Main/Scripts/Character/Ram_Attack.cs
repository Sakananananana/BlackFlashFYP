using System;
using UnityEngine;

public class Ram_Attack : Attack
{
    public Action IsCollidedWithTarget;

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
                IsCollidedWithTarget?.Invoke();
            }
        }
        if (other.CompareTag("Boundary")||other.CompareTag("Exit"))
        {
            IsCollidedWithTarget?.Invoke();
            //Debug.Log("is Boundary");
        }
        if (other.CompareTag(gameObject.tag))
        {
            IsCollidedWithTarget?.Invoke();
            
        }
    }
}

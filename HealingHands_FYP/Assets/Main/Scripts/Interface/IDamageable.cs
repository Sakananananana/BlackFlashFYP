using UnityEngine;

public interface IDamageable
{
    public void RecieveDamage(float damage, Vector3 dmgDir);

    public void Death();
}

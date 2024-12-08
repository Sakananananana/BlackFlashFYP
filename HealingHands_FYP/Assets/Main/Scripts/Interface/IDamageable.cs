using UnityEngine;

public interface IDamageable
{
    void RecieveDamage(float damage, Vector2 dmgDir) { }

    void Death() { }
}

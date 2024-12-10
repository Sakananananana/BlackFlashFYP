using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;

    private void OnEnable()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null && collision.CompareTag("Enemy"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            damageable.RecieveDamage(_player.AttackDamage, dir);
        }
    }

}

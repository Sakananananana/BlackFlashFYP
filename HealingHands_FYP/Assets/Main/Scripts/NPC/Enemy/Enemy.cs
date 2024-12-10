using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _enemyHealth = 20;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    //Damage
    private bool _canTakeDamange = true;

    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        if (_canTakeDamange == true)
        {
            _canTakeDamange = false;
            Debug.Log("Cannot Take Damage" + _canTakeDamange);

            _enemyHealth -= damage;
            StartCoroutine(DamageRecieveCooldown());
        }

        if (_enemyHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator FireProjectile()
    {
        while(_projectile != null)
        {
            yield return new WaitForSeconds(3);

            var projectile = Instantiate(_projectile, _projectileOrigin.position, Quaternion.identity);
        }
    }

    private IEnumerator DamageRecieveCooldown()
    {
        yield return new WaitForSeconds(1f);
        _canTakeDamange = true;
    }

}

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _enemyHealth = 20;

    //need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        _enemyHealth -= damage;

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

}

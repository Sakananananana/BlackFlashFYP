using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    //need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    void Start()
    {
        StartCoroutine(FireProjectile());
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

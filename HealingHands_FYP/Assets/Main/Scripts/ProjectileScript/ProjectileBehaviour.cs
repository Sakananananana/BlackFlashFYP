using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float TravelSpeed;

    private void Awake()
    {
        StartCoroutine(AutoDestroyProjectile());
    }

    private void FixedUpdate()
    {
        //change send in target position
        transform.position = transform.position - transform.right * TravelSpeed * Time.deltaTime;
    }

    private IEnumerator AutoDestroyProjectile()
    {
        yield return new WaitForSeconds(5);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            damageable.RecieveDamage(2, dir);
            DestroyProjectile();
        }
    }
}

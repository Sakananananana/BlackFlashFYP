using System.Collections;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float TravelSpeed;
    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(AutoDestroyProjectile());
    }

    private void FixedUpdate()
    {
        //change send in target position
        _rb2D.linearVelocity = transform.right * TravelSpeed;
        //transform.position = transform.position - transform.right * TravelSpeed * Time.deltaTime;
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

        if (damageable != null && collision.CompareTag("Player"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            damageable.RecieveDamage(2, dir);
            DestroyProjectile();
        }
    }
}

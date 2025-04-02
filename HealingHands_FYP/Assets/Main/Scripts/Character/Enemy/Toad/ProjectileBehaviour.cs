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
        _rb2D.linearVelocity = transform.right * TravelSpeed;
    }

    private IEnumerator AutoDestroyProjectile()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

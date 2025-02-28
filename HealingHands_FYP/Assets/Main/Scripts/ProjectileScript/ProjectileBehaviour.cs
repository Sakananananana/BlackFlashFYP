using System.Collections;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    //Move to Audio Later
    [SerializeField] AudioChannelSO _audioChannelSO;
    [SerializeField] AudioData _splashAudio;
    [SerializeField] AudioConfiguration _audioConfig;

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

            PlayHitProjectileAudio();
            damageable.RecieveDamage(2, dir);
            DestroyProjectile();
        }

        if (collision.CompareTag("Boundary"))
        {
            DestroyProjectile();
        }
    }

    private void PlayHitProjectileAudio() => _audioChannelSO.OnAudioPlayRequested(_splashAudio, _audioConfig);
}
